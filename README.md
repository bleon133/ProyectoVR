# 🎮 La Mansión de los Ecos VR 👻 (Cardboard Móvil)

¡Bienvenido a **La Mansión de los Ecos**, una experiencia de terror psicológico en Realidad Virtual diseñada para jugarse directamente en tu celular con Google Cardboard! 🔦🔊 Sumérgete en una atmósfera low poly cargada de simbolismo religioso, donde cada susurro y cada sombra desvelan un oscuro secreto.  

---

## 📖 Resumen Narrativo

En lo más recóndito de los bosques, se alza una mansión imponente y abandonada, cuyas paredes ocultan una tragedia imposible de olvidar:

> Una familia adinerada y devota vive feliz hasta que su único hijo enferma sin cura posible.  
> Desesperados, descubren un libro prohibido que promete un milagro a cambio de un sacrificio.  
> Al sacrificar al hijo de la criada, su propio hijo vuelve a la vida… pero ya no es el mismo.  
> Poseído por una entidad ancestral, el niño se convierte en un reflejo macabro de su fe rota: ataca y mata a sus padres.  
> La mansión queda abandonada, y hoy quienes se atreven a entrar ven sombras, escuchan pasos… y en el espejo del altar, el reflejo de un niño que jamás debió existir.  

Tú, un investigador escéptico obsesionado con desenmascarar lo paranormal, encuentras archivos que mencionan la desaparición de esta familia y la palabra “**Faith**” como única pista. Convencido de hallar una explicación lógica, te adentras en la mansión… sin saber que aquí la realidad se quiebra y la fe puede ser tu perdición.  

---

## 🕹️ Jugabilidad y Mecánicas Clave

1. ### Exploración Ambiental 🏚️  
   - Recorre cada rincón de la mansión en primera persona usando tu celular y Google Cardboard.  
   - Examina objetos (cartas, símbolos religiosos, grabaciones antiguas, restos de rituales) para armar la historia fragmentada.  
   - Las habitaciones cambian sutilmente con el tiempo: un pasillo que antes estaba vacío puede aparecer lleno de símbolos religiosos la próxima vez que entres.  

2. ### Narrativa Fragmentada 📜  
   - La trama no sigue un solo camino: cada objeto interactivo desbloquea un fragmento de la historia.  
   - Pistas clave (rosarios, páginas manchadas de sangre, grabadoras analógicas) generan flashbacks en tu mente, revelando la desesperación de los padres y el pacto oscuro.  

3. ### Diseño Sonoro como Pilar Central 🎧  
   - El niño poseído se manifiesta principalmente a través del sonido: pisadas tenues, risas distorsionadas, susurros que emergen de la nada.  
   - Audio espacial en Cardboard: cada crujido o susurro provendrá de direcciones específicas, ¡prepárate para girar la cabeza!  

4. ### Mecánica de Sigilo Basada en Ruido 🤫  
   - Cualquier sonido que produces (pasos en el suelo, golpear objetos metálicos, agitar algo) alertará al ente.  
   - Ocúltate en esquinas oscuras o detrás de muebles cuando escuches risas infantiles retorcidas; la criatura dejará de buscar si no detecta movimiento ni eco.  

5. ### Estilo Artístico Low Poly 📐  
   - Modelos simples pero expresivos: baja carga gráfica para garantizar rendimiento fluido en tu Redmi con Cardboard.  
   - Colores desaturados y luces dramáticas para reforzar la sensación de tensión constante.  

---

## 🎯 Objetivos del Juego

- **Sumergirte** en un terror psicológico donde la ambientación y el sonido crean el miedo, sin depender de sustos repentinos (jump scares).  
- **Revelar** la historia de fe, desesperación y sacrificio a medida que exploras y descifras pistas.  
- **Desafiar** tus sentidos: cada crujido o susurro puede decidir tu supervivencia.  

---

## 🔑 Características Destacadas

- **Audio 3D** en Cardboard: siente la cercanía de la entidad gracias a efectos direccionales.  
- **Entorno Dinámico**: habitaciones que mutan, objetos que aparecen o desaparecen según tu progreso narrativo.  
- **Puzles Simbólicos**: resuelve enigmas basados en elementos religiosos para avanzar (cruces, vitrales, reliquias).  
- **Optimización Móvil**: gráficos low poly + calidad de audio ajustada para un Redmi funcionando a 60 FPS en VR.  
- **Controles Intuitivos**: mueve tu cabeza para explorar y pulsa el “botón” del Cardboard para interactuar.  

---

## 📱 Requisitos Técnicos (Android + Cardboard)

1. **Proyecto en Unity**:  
   - Versión recomendada: **Unity 2021.3 LTS** (o superior).  
   - Plataforma de compilación: **Android**.  

2. **Paquetes y Plugins Necesarios**:  
   - **Google Cardboard XR Plugin** (instálalo desde el Package Manager o desde el [repositorio oficial de Cardboard Unity](https://github.com/googlevr/cardboard-xr-plugin)).  
   - **XR Interaction Toolkit** (opcional si quieres controles más avanzados).  
   - **Audio Spatializer Plugin** (por ejemplo, **Resonance Audio** para Android).  

3. **Software Requerido en tu PC**:  
   - **Android SDK & NDK** (instalados via Unity Hub → Android Build Support).  
   - **JDK x64** (OpenJDK incluido en Unity Hub suele ser suficiente).  
   - **Unity Hub** para administrar versiones de Unity y módulos de Android.  

4. **Dispositivo de Prueba**:  
   - **Celular Android (p. ej. Redmi)** con soporte de Google Cardboard (conector de audífonos o Bluetooth).  
   - **Google Cardboard** o visor compatible (asegúrate de ajustar la plantilla de lentes para tu modelo de Redmi).  

---

## 🔧 Cómo Compilar e Instalar en tu Celular

1. **Configurar Proyecto en Unity**  
   - Abre el proyecto `LaMansionDeLosEcos` en Unity 2021.3 (o superior).  
   - En **File → Build Settings**, selecciona **Android** y haz clic en **Switch Platform**.  
   - Verifica que **Google Cardboard XR Plugin** y cualquier plugin de audio estén instalados (Window → Package Manager).  

2. **Ajustes Esenciales en Player Settings**  
   - Ve a **Edit → Project Settings → Player → Other Settings**:  
     - **Package Name**: `com.tuempresa.lamansiondelosecos`  
     - **Minimum API Level**: Android 7.0 “Nougat” (API 24) o superior.  
     - **Target Architecture**: "ARMv7" y "ARM64" para abarcar la mayoría de dispositivos.  
     - **Graphics API**: Vulkan + OpenGLES3 (asegúrate de que tu Redmi lo soporte; si no, deja solo OpenGLES3).  
   - En **XR Plug-in Management**, marca **Cardboard** para la pestaña Android.  

3. **Configuración de Calidad y Rendimiento**  
   - En **Edit → Project Settings → Quality**, crea un perfil “VR_Móvil” con la calidad de textura y sombras reducidas.  
   - Ajusta **Anti-aliasing** a “Disabled” o “2x” para ganar rendimiento.  
   - En la escena principal, revisa todos los materiales y modelos para asegurar que sean **low poly** y usen **texturas ligeras**.  

4. **Build & Run en tu Redmi**  
   - Conecta tu Redmi al PC por USB (activa “Depuración USB” en Opciones de Desarrollador).  
   - En **Build Settings**, haz clic en **Build and Run**.  
   - Selecciona una carpeta donde Unity genere el **APK**.  
   - Una vez compilado, Unity instalará automáticamente el APK en tu Redmi.  

5. **Probar con Google Cardboard**  
   - Coloca tu Redmi en el visor Cardboard. Asegúrate de que el botón físico (o imán) de Cardboard funcione para detectar toques.  
   - Enciende tu Cardboard, abre la app “La Mansión de los Ecos” recién instalada ¡y prepárate para el horror!  

---

## 📲 Controles en Google Cardboard

- 👀 **Mirar / Explorar**:  
  - Gira tu cabeza para ver en 360° los pasillos, habitaciones y objetos.  
- 🕹 **Interactuar / Seleccionar**:  
  - Presiona el botón lateral (imán) o táctil de tu Cardboard para:  
    1. Leer un objeto cercano (cartas, símbolos, grabaciones).  
    2. Encender/apagar la linterna que revela símbolos ocultos en la oscuridad.  
- 🤫 **Sigilo sonoro**:  
  - Evita hacer movimientos bruscos; el niño poseído “escucha” tus pasos (cada movimiento de cabeza produce un ligero sonido).  
  - Si deseas quedarte inmóvil, deja de girar la cabeza y silencia cualquier sonido externo (úsalo con audífonos).  

---

## 👥 Equipo de Desarrollo

| Nombre                         | Rol                                   |
| ------------------------------ | ------------------------------------- |
| **Maicol Steven Florez Rojas** | Programador Unity – Lógica VR & Puzles |
| **Brayan Steven León Martinez**| Programador Unity – Ensamblado y Build Android |
| **Jesus David Diaz Lobo**      | Diseñador de Sonido – Audio 3D, ambiente y susurros |
| **David Esteban Diaz Arguello**| Diseñador de Niveles & Animador – Diseño low poly de entornos |

- **Maicol & Brayan**:  
  - Integración de Google Cardboard XR Plugin, mecánica de ocultamiento y puzles con objetos religiosos.  
  - Optimización de escenas para un **Redmi** en VR a 60 FPS.  
- **Jesus**:  
  - Creación de paisajes sonoros 3D, implementación de audio espacial y diseño de efectos para la entidad.  
- **David**:  
  - Modelado low poly de habitaciones, símbolos religiosos, mobiliario y animaciones básicas de la criatura.  

---
