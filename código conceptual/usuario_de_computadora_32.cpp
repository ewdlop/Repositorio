#include <windows.h>
#include <map>
#include <string>

// Original MessageBoxA function pointer
typedef int (WINAPI *MESSAGEBOXA)(HWND, LPCSTR, LPCSTR, UINT);
MESSAGEBOXA originalMessageBoxA = NULL;

// Spanish translations dictionary
std::map<std::string, std::string> spanishTranslations = {
    {"Error", "Error"},
    {"Warning", "Advertencia"},
    {"Information", "Información"},
    {"Question", "Pregunta"},
    {"OK", "Aceptar"},
    {"Cancel", "Cancelar"},
    {"Yes", "Sí"},
    {"No", "No"},
    {"Abort", "Abortar"},
    {"Retry", "Reintentar"},
    {"Ignore", "Ignorar"},
    {"Help", "Ayuda"}
    // Add more common translations as needed
};

// Function to translate text to Spanish
std::string TranslateToSpanish(const char* text) {
    // First check if we have a direct translation
    if (spanishTranslations.find(text) != spanishTranslations.end()) {
        return spanishTranslations[text];
    }
    
    // If no direct translation, return original text
    // In a real implementation, you would add more sophisticated translation logic here
    return std::string(text);
}

// Hooked MessageBoxA function
int WINAPI SpanishMessageBoxA(HWND hwnd, LPCSTR lpText, LPCSTR lpCaption, UINT uType) {
    // Translate text and caption to Spanish
    std::string spanishText = TranslateToSpanish(lpText);
    std::string spanishCaption = TranslateToSpanish(lpCaption);
    
    // Call original MessageBoxA with translated strings
    return originalMessageBoxA(hwnd, spanishText.c_str(), spanishCaption.c_str(), uType);
}

// DLL Main entry point
// Proxy/hooking 
BOOL APIENTRY DllMain(HMODULE hModule, DWORD reason, LPVOID lpReserved) {
    switch (reason) {
        case DLL_PROCESS_ATTACH: {
            // Get handle to original user32.dll
            HMODULE hUser32 = LoadLibraryA("user32.dll");
            if (hUser32) {
                // Get original MessageBoxA address
                originalMessageBoxA = (MESSAGEBOXA)GetProcAddress(hUser32, "MessageBoxA");
                if (!originalMessageBoxA) {
                    return FALSE;
                }
            } else {
                return FALSE;
            }
            break;
        }
        case DLL_PROCESS_DETACH:
            // Cleanup if needed
            break;
    }
    return TRUE;
}

// Export our Spanish version of MessageBoxA
extern "C" __declspec(dllexport) int WINAPI MessageBoxA(
    HWND hwnd,
    LPCSTR lpText,
    LPCSTR lpCaption,
    UINT uType
) {
    return SpanishMessageBoxA(hwnd, lpText, lpCaption, uType);
}
