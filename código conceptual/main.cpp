#include "pch_español.h"

// Procedimiento de ventana en español
PALABRADOBLE CALLBACK ProcVentanaEsp(
    VENTANA ventana,
    ENTERO mensaje,
    PALABRADOBLE wParam,
    PALABRADOBLE lParam
) {
    switch (mensaje) {
        case WM_CREAR:
            return 0;

        case WM_PINTAR: {
            PAINTSTRUCT ps;
            CONTEXTODISPOSITIVO dc = BeginPaint(ventana, &ps);
            
            RECTANGULO rect;
            GetClientRect(ventana, &rect);
            DibujarTexto(dc, TEXT("¡Hola Mundo!"), -1, &rect, DT_CENTER | DT_VCENTER | DT_SINGLELINE);
            
            EndPaint(ventana, &ps);
            return 0;
        }

        case WM_DESTRUIR:
            PostQuitMessage(0);
            return 0;

        default:
            return DefWindowProc(ventana, mensaje, wParam, lParam);
    }
}

int WINAPI WinMain(
    HINSTANCE hInstance,
    HINSTANCE hPrevInstance,
    LPSTR lpCmdLine,
    int nCmdShow
) {
    // Registrar la clase de ventana
    WNDCLASSEX wc = {0};
    wc.cbSize = sizeof(WNDCLASSEX);
    wc.lpfnWndProc = ProcVentanaEsp;
    wc.hInstance = hInstance;
    wc.lpszClassName = TEXT("VentanaEspañola");
    RegisterClassEx(&wc);

    // Crear la ventana principal
    VENTANA ventanaPrincipal = CrearVentana(
        TEXT("VentanaEspañola"),
        TEXT("Ejemplo de Win32 en Español"),
        WS_VISIBLE | WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT,
        800, 600,
        NULL,
        NULL,
        hInstance,
        NULL
    );

    // Mostrar la ventana
    MostrarVentana(ventanaPrincipal, nCmdShow);
    ActualizarVentana(ventanaPrincipal);

    // Bucle de mensajes
    MENSAJE_ESP msg = {0};
    while (ObtenerMensaje(&msg, NULL, 0, 0)) {
        TraducirMensaje(&msg);
        DispatchMessage(&msg);
    }

    return (int)msg.wParam;
}
