# IslandSurvival30Days
一款环保为主题2D横板生存冒险游戏，玩家需要在荒岛上收集资源、制作物品、建造、生存、与岛上的生物和BOSS战斗、在第30天根据玩家在30天内的选择和环境参数获得不同的结局。
## 游戏内功能
### 剧情系统
使用的技术：Timeline+2D骨骼

![20231107100341](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107100341.png)

### 基本生存系统
![20231107100546](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107100546.png)

- 左上角为人物属性：血量、饥饿、能量
- 左侧为人物背包
- 下方为制作栏（需要解锁）
- 右下角是菜单和人物日志
- 右上角为天数变化

#### 采集与拾取道具

![20231107101030](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107101030.png)

道具分为工具和物品：
- 工具有耐久条不可堆叠。
- 切换工具切换人物动画与逻辑。

![20231107102209](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102209.png)

![20231107102316](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102316.png)

植物和石头的“受击反馈”也通过骨骼动画制作。

物品分为材料和可使用道具：
- 可使用道具有独立的交互功能，例如使用药膏或食物恢复血量、饱食度。
- 材料可以制作物品。

### 战斗系统
随机怪物生成，状态机和行为树制作怪物逻辑，人物骨骼动画和怪物碰撞体用于攻击检测和事件绑定，相机震动表现受击反馈。
怪物死亡随机生成战利品，作为玩家的道具和材料，与背包和制作系统联动。

![20231107101541](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107101541.png)

![20231107101512](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107101512.png)


分阶段BOSS：

![20231107102824](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102824.png)

![20231107102859](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102859.png)

### NPC交互

![20231107102411](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102411.png)

与NPC交互可以推动剧情、改变游戏参数（影响结局或解锁制作配方）、交易。

![20231107102527](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102527.png)

![20231107102544](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102544.png)


![20231107102643](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107102643.png)

### 建造与制作系统
建筑分为室内建筑和室外建筑，在室外消耗材料建筑房子、火堆、栅栏等建筑。

![20231107103232](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107103232.png)

> 图中的篝火可以消耗木材添火，篝火可以制作烤肉，篝火的亮度会随着时间变化（室内火炉同理）。

房子可以进入，在室内可以建造床、火炉、沙发等建筑。

![20231107103417](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107103417.png)

床可以用于睡觉，恢复血量和体力，消耗饥饿值。

![20231107103519](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107103519.png)

### 生态值系统
游戏内砍树、屠杀生物降低生态值，种植、用栅栏圈养生物、喂食生物可以恢复生态值，生态值影响怪物刷新和游戏氛围以及结局。

![20231107103655](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107103655.png)


![20231107103748](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107103748.png)

### UI和结局系统

![20231107104303](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104303.png)

9个可收集结局拼图

![20231107104155](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104155.png)

图鉴系统

![20231107104221](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104221.png)

![20231107104327](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104327.png)

### 地形、光照和后处理
使用Unity2D光照管线和材质，一些游戏内物体使用法线贴图增强光照效果。
地形由Shape Render和Edge Colider制作。

![20231107104525](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104525.png)

![20231107104626](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104626.png)

![20231107104856](https://cdn.jsdelivr.net/gh/Magic0Conch/gallery/blogs/pictures/20231107104856.png)

后处理使用插件完成，包括屏幕边框变红、色调映射和老电影效果。
