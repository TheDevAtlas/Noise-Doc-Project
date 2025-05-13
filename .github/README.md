<h1 align="center">
  <a>
    <picture>
      <source height="125" media="(prefers-color-scheme: dark)" srcset="readmetitle.png">
      <img height="125" alt="Fiber" src="readmetitle.png">
    </picture>
  </a>
</h1>

## 🌊 Abstract

This project creates visuals using the animation and rendering capabilities of Unity. Utilizing the Universal Render Pipeline (URP), the project produces high-quality animations that show the versatility of noise functions in creating organic, natural-looking movement and patterns.

Perlin noise, developed by Ken Perlin in 1983, is a gradient noise function that produces a more natural, harmonic appearance compared to simple random functions. This project leverages this characteristic to generate visually appealing animations that can be used for various purposes, including game development, visual effects, and artistic installations.

While these visualizations were left unused in an old product, they are a good example of what can be done with non-interactive environments.

## ⚙️ Installation & Setup

This project requires **Unity 6000 LTS or higher** with the Universal Render Pipeline package installed. Follow these steps to set up the project:

1. **Clone the repository**:
   ```bash
   https://github.com/TheDevAtlas/Noise-Doc-Project
   ```

2. **Open with Unity Hub**:
   - Launch Unity Hub
   - Click "Add" and navigate to the cloned repository folder
   - Select the project folder and open it with the appropriate Unity version

3. **Verify Required Assets**:
   - Universal Render Pipeline package
   - Unity Recorder package

## 🎬 Project Explanation

### Core Components

This project uses scenes to split up the visualizations:

1. **Perlin Plane**: 
   - Generates an NxN grid that is displaced in a wave pattern.
   - Uses Perlin noise to displace the plane.

2. **Minecraft Chunk**:
   - Minecraft chunk generation animation
   - Uses Perlin noise to simulate placement of types of blocks.
   - Animation uses timers and cubic Béziers to interpolate animations.

3. **Generic Animations**:
   - Two separate animations for "voice over" sections.
   - These have visuals of dice rolls, floating numbers, and computer consoles.