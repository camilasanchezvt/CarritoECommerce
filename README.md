# DeliveryGo 🛒

Mini-ecommerce en consola desarrollado en **C#** como trabajo integral de **Patrones de Diseño** (Creacionales, Estructurales y de Comportamiento) para la materia **Programación**.

---

## 📌 ¿Qué hace el programa?
DeliveryGo simula un sistema de compras online en formato de consola.  
Permite al usuario:

- ➕ Agregar productos al carrito 🛒  
- ➖ Eliminar ítems o deshacer acciones con **UNDO** ↩️  
- 🚚 Elegir tipo de envío  
- 💲 Calcular el costo final de la compra  
- ✅ Confirmar y pagar el pedido  

---

## 🛠️ Tecnologías usadas
- Lenguaje: **C#**  
- Framework: **.NET 5.0**  
- IDE: **Visual Studio Community**  

---

## 🎯 ¿Qué necesidad satisface?
El programa satisface la necesidad de **gestionar compras simples en línea**, mostrando cómo aplicar **patrones de diseño** a un flujo de ecommerce.

---

## 🌍 Ámbitos de aplicación
Este sistema puede adaptarse a distintos rubros:

- 🛒 Tiendas online de productos físicos (supermercados, librerías, ropa).  
- 🍔 Servicios de delivery (comida, bebidas, farmacia).  
- 🎟️ Aplicaciones de reserva o compra de entradas.  

---

## 🚀 Cómo usar la aplicación
1. Clonar o descargar el repositorio.  
2. Abrir el proyecto en **Visual Studio / Rider / VS Code** con **.NET SDK instalado**.  
3. Compilar el proyecto.  
4. Ejecutar en consola (o con debug del IDE):  

   ```bash
   dotnet run
   ```
   ### Seguir las opciones del menú
- ➕ **Agregar ítems**  
- ➖ **Eliminar ítems / UNDO**  
- 🚚 **Elegir envío**  
- ✅ **Pagar y confirmar**  

---

## 👥 Integrantes del equipo
- **Giuliana Manzo** →   
  - Responsabilidad: *ETAPA 1 – desarrollo de la lógica de carrito y menú*.  

- **Yamila Sanchez** →   
  - Responsabilidad: *ETAPA 2 – implementación de patrones Command y Strategy*.  

- **Valentina Olmos** →   
  - Responsabilidad: *ETAPA 3 – manejo de persistencia y documentación*.  

---

## 🧩 Patrones aplicados
- **Command** → Para implementar la función UNDO y gestionar acciones del carrito.  
- **Strategy** → Para definir distintos tipos de envíos y sus costos.  
- **Singleton** → Para la instancia única del carrito de compras.  
- **Facade** → Para simplificar la interacción entre subsistemas (carrito, envío, pago).  

---

## 📖 Caso narrado de uso
1. El usuario ingresa al programa y agrega un **producto A**.  
2. Se equivoca y lo elimina usando la opción **UNDO**.  
3. Luego agrega **producto B** y **producto C**.  
4. Selecciona el tipo de envío (ej: envío express).  
5. El sistema calcula el precio final con envío incluido.  
6. El usuario confirma y paga el pedido.  
7. El sistema muestra un mensaje de confirmación y finaliza la compra.  

---

## 🗺 UML
📌 ![Diagrama UML]()  

---

## 🔮 Retos futuros
- 👥 Agregar soporte para múltiples usuarios.  
- 💳 Implementar distintos métodos de pago (tarjeta, billetera virtual).  
- 📂 Agregar persistencia usando JSON.  
- 🖥️ Añadir interfaz gráfica.  

---

## 📌 Notas finales
DeliveryGo demuestra cómo aplicar **Patrones de Diseño** en un caso realista de ecommerce.  
Fue desarrollado como **trabajo integral de Programación II**, reforzando el aprendizaje de **arquitectura limpia, patrones y buenas prácticas en C#**. 
