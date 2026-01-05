# 🧩 3D Maze Runner – AI Gameplay Project (PC)

---

## 🎮 Project Overview

**3D Maze Runner** is a **PC-based third-person / first-person Unity game** focused on **AI behavior, maze navigation, and combat mechanics**.

The project was designed as a **training and research-oriented gameplay system**, emphasizing:

* **Custom enemy AI logic**
* **Maze-based level structure**
* **Player–enemy interaction**
* **No NavMesh usage** (pure script-driven AI)

All characters—including the **player and enemy robots**—are built entirely using **Unity primitive shapes**, highlighting logic and systems over visual assets.

---

## 🏗️ Project Structure

The project follows a clean Unity asset organization:

```
Assets/
├── Animation/        → Enemy & player animations
├── Materials/        → Maze walls, characters, environment
├── Prefabs/          → Player, enemy, projectile, medikit
├── Scenes/           → Maze gameplay scene
├── Scripts/          → Core gameplay & AI logic
├── Textures/         → Basic textures for environment
```

This structure keeps **gameplay logic, visuals, and prefabs decoupled**, making the project easy to extend and debug.

---

## 🧠 Maze Environment Design

* Fully **enclosed 3D maze**
* Narrow corridors and intersections
* Walls used as **line-of-sight blockers**
* Designed to:

  * Test AI perception
  * Limit visibility
  * Force close-range encounters
* Maze layout directly affects:

  * Enemy detection
  * Combat difficulty
  * Player survival strategy

The maze itself acts as a **core gameplay mechanic**, not just a level.

---

## 🤖 Characters (Built from Primitive Shapes)

### 👤 Player (BayMax Controller)

* Constructed using **Unity cubes, capsules, and spheres**
* First-person shooting + free movement
* Mouse-based camera control
* Health system with recovery items

### 👾 Enemy Robots

* Fully procedural humanoid robots
* Head, body, and mouth constructed from primitives
* Animations driven via **state-based logic**

This design keeps the focus on **behavior and AI**, not assets.

---

## 🧠 Enemy AI System (No NavMesh)

> ⚠️ **No NavMesh or Unity AI Navigation used**

Enemy behavior is implemented using **custom scripting only**.

### 🔁 AI State Machine

Each enemy operates using a **finite state machine**:

| State        | Behavior                           |
| ------------ | ---------------------------------- |
| Idle         | Enemy waits and scans surroundings |
| Walking      | Moves forward through maze         |
| TurnAround   | Rotates when obstacle detected     |
| AttackPlayer | Attacks when player detected       |
| Dead         | Death animation and cleanup        |

---

### 👁️ Player Detection (Custom Vision System)

* Enemy uses **multi-ray raycasting** (field-of-view scanning)
* ~90 rays cast in a cone pattern
* Detects player **only if visible**
* Walls and maze geometry block vision naturally

This simulates **realistic AI perception** without pathfinding.

---

### 🚶 Movement & Obstacle Avoidance

* Forward movement only
* Obstacle detection via raycasts
* On collision:

  * Enemy rotates by a random angle
  * Continues navigation
* This creates **emergent maze navigation behavior**

---

### 🔫 Enemy Attacks

* Enemy attacks only when:

  * Player is in view
  * Line of sight is clear
* Uses raycasting from mouth position
* Deals continuous damage while attacking

---

## 🎯 Player Gameplay Mechanics

### 🕹️ Movement

* WASD keyboard input
* Smooth directional movement
* Mouse-based camera rotation
* Cursor locked for immersion

### 🔫 Shooting

* Projectile-based combat
* Physics-driven bullets
* Collision-based damage system

### ❤️ Health System

* Player has limited health
* Enemies reduce health on contact
* **Medikit pickups** restore health
* Medikit respawns after cooldown

---

## 👾 Enemy Spawning System

* Enemies spawn at **random maze spawn points**
* Spawn locations are removed once used
* Ensures:

  * No enemy overlap
  * Balanced maze coverage
* Total enemy count configurable

---

## 🧪 Technical Highlights

* ❌ No NavMesh
* ❌ No AI Pathfinding package
* ✅ Raycast-based perception
* ✅ State-driven AI
* ✅ Event-based input system
* ✅ Physics-based projectiles
* ✅ Modular gameplay scripts

---

## 🎯 Learning & Research Focus

This project demonstrates:

* Custom AI design
* Maze-based decision making
* Enemy perception without pathfinding
* State machines in gameplay AI
* Unity gameplay architecture fundamentals

It is ideal as:

* AI training project
* Gameplay systems demo
* Portfolio project for Unity / game AI roles

---

## 🖥️ Platform

* **PC (Windows)**
* Unity Engine
* Keyboard + Mouse controls

---

## 🚀 Future Improvements

Potential extensions:

* Smarter pursuit behavior
* Cooperative enemy AI
* Dynamic maze generation
* Difficulty scaling
* Sound-based enemy detection

---

## 📌 Summary

**3D Maze Runner** is a **systems-focused Unity project** that showcases:

* Custom AI behavior
* Maze-driven gameplay design
* Combat mechanics
* Clean gameplay architecture

All built with **simple geometry and pure logic**, making the AI behavior the star of the project.

