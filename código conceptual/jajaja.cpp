#include <windows.h>
#include <stdio.h>

// Function to change system language
BOOL ChangeSystemLanguage(LANGID languageId) {
    HKEY hKey;
    LONG result;
    
    // Open registry key for language settings
    result = RegOpenKeyEx(
        HKEY_CURRENT_USER,
        L"Control Panel\\International",
        0,
        KEY_WRITE,
        &hKey
    );
    
    if (result != ERROR_SUCCESS) {
        printf("Error opening registry key: %ld\n", result);
        return FALSE;
    }

    // Get language name from language ID
    wchar_t localeName[LOCALE_NAME_MAX_LENGTH];
    if (!LCIDToLocaleName(MAKELCID(languageId, SORT_DEFAULT), 
                         localeName, 
                         LOCALE_NAME_MAX_LENGTH, 
                         0)) {
        printf("Error getting locale name\n");
        RegCloseKey(hKey);
        return FALSE;
    }

    // Set new language values in registry
    result = RegSetValueEx(
        hKey,
        L"LocaleName",
        0,
        REG_SZ,
        (BYTE*)localeName,
        (wcslen(localeName) + 1) * sizeof(wchar_t)
    );

    if (result != ERROR_SUCCESS) {
        printf("Error setting registry value: %ld\n", result);
        RegCloseKey(hKey);
        return FALSE;
    }

    // Broadcast the change to all windows
    SendMessageTimeout(
        HWND_BROADCAST,
        WM_SETTINGCHANGE,
        0,
        (LPARAM)L"Environment",
        SMTO_ABORTIFHUNG,
        5000,
        NULL
    );

    RegCloseKey(hKey);
    return TRUE;
}

int main() {
    // Example: Change to Spanish (Spain)
    LANGID spanishLangId = MAKELANGID(LANG_SPANISH, SUBLANG_SPANISH);
    
    if (ChangeSystemLanguage(spanishLangId)) {
        printf("Language changed successfully. You may need to log out and back in.\n");
    } else {
        printf("Failed to change language.\n");
    }

    // List available languages
    printf("\nAvailable languages:\n");
    WCHAR langBuffer[256];
    
    EnumSystemLocalesEx([](LPWSTR lpLocaleName, DWORD dwFlags, LPARAM lParam) -> BOOL {
        wchar_t displayName[256];
        GetLocaleInfoEx(lpLocaleName, LOCALE_SLOCALIZEDDISPLAYNAME, displayName, 256);
        wprintf(L"%s (%s)\n", displayName, lpLocaleName);
        return TRUE;
    }, LOCALE_ALL, 0, 0);

    return 0;
}
