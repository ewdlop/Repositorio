#ifndef WIN32_ESPAÑOL_H
#define WIN32_ESPAÑOL_H

#include <windows.h>

#ifdef __cplusplus
extern "C" {
#endif

// Tipos de datos básicos en español
typedef BOOL BOOLEANO;
typedef BYTE BYTE_ESP;
typedef CHAR CARACTER;
typedef WORD PALABRA;
typedef DWORD PALABRADOUBLE;
typedef FLOAT FLOTANTE;
typedef HANDLE MANEJADOR;
typedef INT ENTERO;
typedef LONG LARGO;
typedef SHORT CORTO;
typedef VOID VACIO;

// Ventana y mensajes
typedef HWND VENTANA;
typedef HMENU MENU;
typedef HACCEL ACELERADOR;
typedef HDC CONTEXTODISPOSITIVO;
typedef MSG MENSAJE;

// Constantes de MessageBox en español
#define MB_ACEPTAR          MB_OK
#define MB_CANCELAR         MB_CANCEL
#define MB_SI              MB_YES
#define MB_NO              MB_NO
#define MB_REINTENTAR      MB_RETRY
#define MB_ABORTAR         MB_ABORT
#define MB_IGNORAR         MB_IGNORE
#define MB_AYUDA           MB_HELP

// Estilos de ventana en español
#define WS_BORDE           WS_BORDER
#define WS_CAPTIONBAR      WS_CAPTION
#define WS_HIJO            WS_CHILD
#define WS_VISIBLE         WS_VISIBLE
#define WS_MINIMIZAR       WS_MINIMIZE
#define WS_MAXIMIZAR       WS_MAXIMIZE

// Mensajes de ventana en español
#define WM_CREAR           WM_CREATE
#define WM_DESTRUIR        WM_DESTROY
#define WM_PINTAR          WM_PAINT
#define WM_CERRAR          WM_CLOSE
#define WM_COMANDO         WM_COMMAND
#define WM_TAMAÑO          WM_SIZE
#define WM_MOVER           WM_MOVE
#define WM_RATON_MOVER     WM_MOUSEMOVE
#define WM_RATON_CLICK     WM_LBUTTONDOWN
#define WM_RATON_DCLICK    WM_LBUTTONDBLCLK
#define WM_TECLADO_ABAJO   WM_KEYDOWN
#define WM_TECLADO_ARRIBA  WM_KEYUP

// Estructuras en español
typedef struct tagPUNTO {
    LARGO x;
    LARGO y;
} PUNTO, *PPUNTO;

typedef struct tagRECTANGULO {
    LARGO izquierda;
    LARGO arriba;
    LARGO derecha;
    LARGO abajo;
} RECTANGULO, *PRECTANGULO;

typedef struct tagMSG_ESP {
    VENTANA   ventana;
    ENTERO    mensaje;
    PALABRADOUBLE    wParam;
    PALABRADOBLE    lParam;
    PALABRADOBLE    tiempo;
    PUNTO     puntoRaton;
} MENSAJE_ESP, *PMENSAJE_ESP;

// Funciones de ventana en español
BOOLEANO WINAPI CrearVentana(
    LPCTSTR nombreClase,
    LPCTSTR titulo,
    PALABRADOBLE estilo,
    ENTERO x,
    ENTERO y,
    ENTERO ancho,
    ENTERO alto,
    VENTANA padre,
    MENU menu,
    HINSTANCE instancia,
    LPVOID param
);

BOOLEANO WINAPI MostrarVentana(
    VENTANA ventana,
    ENTERO mandoMostrar
);

BOOLEANO WINAPI ActualizarVentana(
    VENTANA ventana
);

// Funciones de mensaje en español
ENTERO WINAPI CajaMensaje(
    VENTANA    padre,
    LPCTSTR    texto,
    LPCTSTR    titulo,
    ENTERO     tipo
);

BOOLEANO WINAPI ObtenerMensaje(
    PMENSAJE_ESP mensaje,
    VENTANA      ventana,
    ENTERO       mensajeMin,
    ENTERO       mensajeMax
);

BOOLEANO WINAPI TraducirMensaje(
    CONST MENSAJE_ESP *mensaje
);

PALABRADOBLE WINAPI EnviarMensaje(
    VENTANA      ventana,
    ENTERO       mensaje,
    PALABRADOBLE wParam,
    PALABRADOBLE lParam
);

// Funciones de GDI en español
CONTEXTODISPOSITIVO WINAPI ObtenerDC(
    VENTANA ventana
);

ENTERO WINAPI LiberarDC(
    VENTANA ventana,
    CONTEXTODISPOSITIVO dc
);

BOOLEANO WINAPI DibujarTexto(
    CONTEXTODISPOSITIVO dc,
    LPCTSTR texto,
    ENTERO   longitud,
    RECTANGULO *rect,
    ENTERO   formato
);

// Macros útiles
#define HACER_PALABRADOBLE(bajo, alto) ((PALABRADOBLE)(((PALABRA)(bajo)) | ((PALABRADOBLE)((PALABRA)(alto))) << 16))
#define OBTENER_X_PARAM(param) ((ENTERO)(CORTO)LOWORD(param))
#define OBTENER_Y_PARAM(param) ((ENTERO)(CORTO)HIWORD(param))

// Declaraciones de callback
typedef PALABRADOBLE (CALLBACK* PROCVENTANA_ESP)(
    VENTANA ventana,
    ENTERO mensaje,
    PALABRADOBLE wParam,
    PALABRADOBLE lParam
);

#ifdef __cplusplus
}
#endif

#endif // WIN32_ESPAÑOL_H
