<h1 align="center">
  <a href="https://gofiber.io">
    <picture>
      <source height="125" media="(prefers-color-scheme: dark)" srcset="https://raw.githubusercontent.com/gofiber/docs/master/static/img/logo-dark.svg">
      <img height="125" alt="Fiber" src="https://raw.githubusercontent.com/gofiber/docs/master/static/img/logo.svg">
    </picture>
  </a>
</h1>

## 🌊 Abstract

This Unity project explores the artistic and visual potential of Perlin noise by creating dynamic, procedurally generated visualizations. Utilizing Unity's Universal Render Pipeline (URP), the project produces high-quality animations that demonstrate the versatility of noise functions in creating organic, natural-looking movement and patterns.

Perlin noise, developed by Ken Perlin in 1983, is a gradient noise function that produces a more natural, harmonic appearance compared to simple random functions. This project leverages this characteristic to generate visually appealing animations that can be used for various purposes, including game development, visual effects, and artistic installations.

While these visualizations were ultimately unused in the final product, they demonstrate the effective use of Unity's systems and tools to create compelling procedural content. The project showcases techniques that can be applied to terrain generation, particle effects, texture creation, and animation patterns.

## ⚙️ Installation & Setup

This project requires **Unity 2021.3 LTS or higher** with the Universal Render Pipeline package installed. Follow these steps to set up the project:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/unity-perlin-noise-visuals.git
   ```

2. **Open with Unity Hub**:
   - Launch Unity Hub
   - Click "Add" and navigate to the cloned repository folder
   - Select the project folder and open it with the appropriate Unity version

3. **Verify URP Installation**:
   - In Unity, go to Window > Package Manager
   - Ensure Universal RP package is installed (version 12.0 or higher)
   - If not installed, add it from the Unity Registry

4. **Import Required Assets**:
   - Unity Recorder package (for capturing animations)
   - Post-processing package (if not included with URP)

5. **Project Configuration**:
   - The project uses a URP Asset located in `Assets/Settings/UniversalRP-HighQuality.asset`
   - Ensure Graphics settings are using this URP Asset in Project Settings

6. **Scene Setup**:
   - Open the sample scenes in the `Assets/Scenes` folder
   - Each scene demonstrates different Perlin noise visualizations

## 🎬 Project Explanation

### Core Components

This project consists of several key components that work together to create dynamic Perlin noise visualizations:

1. **Noise Generators**: 
   - Custom C# scripts that implement various noise algorithms
   - Configurable parameters to control frequency, amplitude, and octaves
   - Support for both 2D and 3D noise implementations

2. **Visual Renderers**:
   - Shader Graph implementations for rendering noise as visual effects
   - Material systems that respond to noise parameters
   - Mesh manipulators that use noise for geometric deformation

3. **Animation Controllers**:
   - Systems to animate noise parameters over time
   - Keyframe-based animation curves for precise control
   - Event-based triggers for synchronized visual effects

4. **Recording System**:
   - Integration with Unity Recorder for high-quality output
   - Preset configurations for different output formats (MP4, PNG sequence, etc.)
   - Batch rendering capabilities for generating multiple visualizations

### Key Features

- **Real-time Preview**: Interactive controls to adjust noise parameters in the editor
- **Modular Design**: Easily combine different noise functions for complex effects
- **Optimized Performance**: Efficient implementation for real-time applications
- **Flexible Output**: Support for various resolutions and output formats
- **Custom Inspector Tools**: Editor scripts for improved workflow and usability

### Implementation Details

The project uses a combination of compute shaders and the GPU to generate Perlin noise efficiently, allowing for real-time manipulation and rendering. The implementation includes:

- Octave-based noise generation for added detail and natural appearance
- Domain warping techniques for more complex and interesting patterns
- Custom noise seeds for reproducible results
- Time-based animation for dynamic, evolving visualizations

## 👥 Contributors, Resources & Citations

### Contributors

- **Main Developer**: Your Name - Concept, implementation, and documentation
- **Special Thanks**:
  - Unity Technologies for their excellent rendering tools
  - The open-source community for inspiration and shared knowledge

### Resources

The development of this project relied on several key resources:

- **Unity Documentation**: 
  - [Universal Render Pipeline](https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@12.0/manual/index.html)
  - [Unity Recorder](https://docs.unity3d.com/Packages/com.unity.recorder@3.0/manual/index.html)

- **Tools Used**:
  - Unity 2021.3 LTS
  - Universal Render Pipeline
  - Unity Recorder
  - Visual Studio / VS Code for scripting

### Citations

- Perlin, K. (1985). "An Image Synthesizer". SIGGRAPH Computer Graphics, 19(3), 287-296.
- Gustavson, S. (2005). "Simplex noise demystified". Linköping University, Sweden.
- Unity Technologies. (2022). "Introduction to Shader Graph". Unity Documentation.
- Flick, J. (2020). "Noise Algorithms". Catlike Coding Tutorials.

### License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 🚀 Future Developments

While these visualizations were not used in the final project, potential future developments include:

- Integration with audio analysis for sound-reactive visuals
- Expanded toolset for artistic experimentation
- Performance optimizations for mobile platforms
- VFX Graph integration for more complex visual effects

---

**Note**: This project is provided as-is for educational and reference purposes. Feel free to fork and modify it for your own projects.