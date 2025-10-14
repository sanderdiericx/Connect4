# **Connect 4 Built on a Custom Rendering Engine**

A complete Connect 4 game built in **C#**, using **WinForms** for window management and input handling.  
All rendering is done **pixel by pixel**, with no external graphics libraries used.

<img width="1380" height="957" alt="Connect 4 14_10_2025 22_58_05" src="https://github.com/user-attachments/assets/4aa30805-da8c-4328-9fb1-27a254c80b78" />
![Demo](https://github.com/user-attachments/assets/6c9c54b2-9c5a-4e66-84c4-83e786b5a69d)

---

## **Key Features**

### **Gameplay**
- Classic Connect 4 mechanics
- Intuitive controls  
- Modular structure with support for multiple game states  

### **Rendering**
- Bitmap pixel manipulation by writing directly to memory for improved performance  
- Sprite system with three core primitives: **Circle**, **Square**, and **Triangle**  
- Simple transformations (currently limited to position, as that was sufficient for this project)  
- Customizable sprite properties such as border size, border color, fill color, and visibility  
- Animation system supporting both transform and color animations  

---

## **Architecture**

### **Rendering System**
- A **RenderBatch** object holds all sprites to be rendered each frame the engine automatically draws all sprites within this batch  
- Each sprite type is rasterized directly into a bitmap buffer  
- The final bitmap is drawn to the WinForms window every frame  
- Rendering is entirely software based, with no reliance on GPU acceleration  

### **Animation System**
- Each animation defines its **target sprite**, **speed**, and an **easing function** that controls how the animation behaves  
- Two animation types are currently supported: **Transform Animations** and **Color Animations**, each with their own parameters  
- Animations are updated using **linear interpolation (lerp)**  
- **AnimationChains** allow you to sequence multiple animations or create infinitely looping effects  

In the Connect 4 game, these animations are used to:
1. Create a bouncing effect when dropping markers  
2. Highlight the winning sequence of markers  
3. Fade out non winning markers using transparency  

---

## **Running the Project**

1. Clone this repository  
2. Open the solution in **Visual Studio 2022**  
3. Build and run the project  

---

## **Future Improvements**

In the future, I’d like to improve and expand the project by:
- Refining triangle rasterization logic  
- Adding sprite scaling and rotation  
- Implementing animation support for scaling and rotation  

---

## **Final Notes**

This project was built as part of a school assignment to create a Connect 4 game in WinForms.  
We were given a lot of freedom in how we could build it, so I used the opportunity to improve my skills on project structure and low level 2D graphics rendering.  

It was a great way to strengthen my understanding of software rendering, animation systems, and game architecture.  

Feel free to reach out or check out my other projects!
