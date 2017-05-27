
using Engine;
using Engine.Events.MouseEvent;
using Engine.Grid;
using Engine.Interfaces.Cam;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Render;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Screen;
using Engine.Interfaces.Sound;
using Engine.Managers.Cam;
using Engine.Managers.Collision;
using Engine.Managers.Render;
using Engine.Managers.ServiceLocator;
using Engine.States.Engine;
using Engine34.Entities.Animation;
using Engine34.Entities.Animation.Debug;
using Engine34.Managers.Cam;
using Engine34.Managers.Render;
using GameCode.Entities.Misc.Scenery.Animated.Torch;
using GameCode.Entities.Platformer;
using GameCode.Entities.Spawner;
using GameCode.Entities.Steering;
using GameCode.Items;
using GameCode.Items.Management;
using GameCode.MapGeneration;
using GameCode.Shaders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameCode.Screens.Levels
{
    class Dungeon1 : BaseScreen
    {
        Grids grid;
        int Density;
        EntitySpawner eS;
        SpawnGroup sg;
        int Difficulty = 5;
        ItemManager test;
        bool loadComplete = false;
        bool itemDropped = false;
        bool bossDropped = false;
        int torchAmount = 0;
        WaveGenerator wave;

        IEntity p;
        Player player;
        Vector2 offset = new Vector2(-450, -400);

        bool spawned = false;

        List<TorchProp> TorchList = new List<TorchProp>();

   

        public override void Initialize()
        {
          
            // SoundTrack = "MenuSong";
            GameInformation.currentScreen = this;

            Locator.Instance.getService<ISoundManager>().Volume(0.10f);
            //ITEM MANAGER
            test = new ItemManager();
            GameTools.GetItemManager = test;
            //MAP GRID AND SPAWNER
            setMapGrid(30, 30);
            setSpawner();
            //SHADER STUFF
            Shader st = new AnotherTestShader();
            Locator.Instance.getService<IRenderManager>().AddShader(st);
            //LIGHTING STUFF           
            for (int i = 0; i < torchAmount; i++)
            {

                //Create a bunch of torches
                Vector2 randomVector = new Vector2(Constants.r.Next(0, 2000), Constants.r.Next(0, 2000));
                TorchProp pro = new TorchProp();
                pro.Initialize(randomVector);
                TorchList.Add(pro);
            }



            //  Locator.Instance.getService<ICameraManager>().getCam().setEntity(Locator.Instance.getService<IEntityManager>().createEntityDrawable<TestEntity>(Vector2.Zero), CameraState.FOLLOW);


              Locator.Instance.getService<ICameraManager>().getCam().setEntity(Locator.Instance.getService<IEntityManager>().createEntityDrawable<pEntity>(Vector2.Zero), CameraState.LOCKED);


            loadComplete = true;

            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseClick;


            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Density <= 0 && loadComplete)
            {
                Density = 0;
                if (bossDropped == false)
                {
                    bossDropped = true;
                    //  eS.sendBoss(2, true, 12);
                }
                // Locator.Instance.getService<IDetectionManager>().ClearCollisionList();
                //Locator.Instance.getService<IEntityManager>().clearList();
                // Locator.Instance.getService<IScreenManager>().RemoveTopScreen();
                int offset = 0;

            }
            //  wave.Update();
            // System.Console.WriteLine(Locator.Instance.getService<IDetectionManager>().);
            for (int i = 0; i < TorchList.Count; i++)
            {
                TorchList[i].Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (grid != null)
                grid.Draw(spriteBatch);
            test.Draw(spriteBatch);
            Engine.Utility.DrawString.Draw("DENSITY : " + Density, spriteBatch, Vector2.Zero, Color.White, 10f);
            for (int i = 0; i < TorchList.Count; i++)
            {
                TorchList[i].Draw(spriteBatch);
            }
            base.Draw(spriteBatch);
        }

        public override void Unload()
        {
            Locator.Instance.getService<IDetectionManager>().resetCollisionLayer();
            Locator.Instance.getService<IEntityManager>().clearList();
            Locator.Instance.getService<IRenderManager>().clearTempEntity();
            Locator.Instance.getService<ILightMaskManager>().ClearLightMasks();
            TorchList = null;
            base.Unload();
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            // Locator.Instance.getService<ILightMaskManager>().addMask(new LightMask(new Vector2(e.boundsToWorldView.X-150, e.boundsToWorldView.Y-150), Locator.Instance.getService<IResourceLoader>().GetTex("Grad")));
        }


        public void spawnAtSuitable(CA conv)
        {
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    if (grid.getGrid[i, j].Color == Color.PaleVioletRed)
                    {

                        if (conv.GetSurroundingWallCount(i, j) < 1 && !spawned)
                        {
                            spawned = true;
                            // p = Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<pEntity>(new Vector2(j * grid.getGrid[i, j].Bounds.Width, i * grid.getGrid[i, j].Bounds.Height));
                        }
                    }


                }
            }




            //   Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<pEntity>(new Vector2(0, 0));


            Locator.Instance.getService<ICameraManager>().getCam().setEntity(Locator.Instance.getService<IEntityManager>().getCamEntity("Player"), CameraState.LOCKED);

        }

        public void setMapGrid(int width, int height)
        {
            grid = new Grids(Locator.Instance.getService<IResourceLoader>().GetTex("Tile3"));
            grid.create(width, height);
            grid.setupVisual();
            CA conv = new CA();
            Maze maze = new Maze();
            grid = conv.GenerateCA(grid);


          //  spawnAtSuitable(conv);

        }

        public void setSpawner()
        {

            eS = new EntitySpawner(1000, new Vector2(500, 500), this);
            sg = new PrototypeSpawn();

            wave = new WaveGenerator();
            wave.setSpawnGroup(sg);
            wave.setDifficulty(5 * Difficulty);
            wave.setWaves(6);
            wave.setWaveTime(500);
            wave.Begin();
        }
    }
}
