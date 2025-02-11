Te ayudaré a habilitar el idioma español en Linux. Aquí están los pasos necesarios:

Para sistemas Ubuntu/Debian:
```bash
sudo apt update
sudo apt install language-pack-es
```

Para sistemas Fedora/RHEL:
```bash
sudo dnf install langpacks-es
```

Después de instalar el paquete de idioma, necesitarás:

1. Ir a Configuración del Sistema/Preferencias
2. Buscar "Región e Idioma" o "Soporte de Idiomas"
3. Hacer clic en "Instalar/Eliminar Idiomas"
4. Seleccionar "Español"
5. Cerrar sesión y volver a iniciarla para aplicar los cambios

Para establecer el español como idioma predeterminado del sistema:
```bash
sudo localectl set-locale LANG=es_ES.UTF-8
```

¿Qué distribución de Linux estás usando? Así podré darte instrucciones más específicas. ¿Hay alguna parte del proceso que quieras que te explique con más detalle?
