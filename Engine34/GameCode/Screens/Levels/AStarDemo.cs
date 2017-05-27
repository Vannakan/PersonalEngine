using Engine.Managers.AStar;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Events.MouseEvent;
using Engine.Events.KeyboardEvent;
using Engine.States.Engine;
using Engine.Interfaces.Resource;
using Engine.Managers.ServiceLocator;
using Engine.Grid;
using Engine.Interfaces.Cam;
using Engine.Interfaces.Entities;
using Engine.Interfaces.Behaviour;
using GameCode.Entities.Steering;

namespace GameCode.Screens.Levels
{
    public class AStarDemo : BaseScreen
    {
        Grids grid;
        AstarPath search1;
        AStarGridSearch a;
        Random random;
        int max = 20;

       SteeringMind test;


        public override void Initialize()
        {

            Locator.Instance.getService<ICameraManager>().getCam().Zoom = 1f;

            Locator.Instance.getService<KeyHandler>().KeyDown += OnKeyDown;

            Locator.Instance.getService<MouseHandler>().MouseClick += OnMouseClick;
            random = new Random();
            search1 = new AstarPath();
            grid = new Grids(Locator.Instance.getService<IResourceLoader>().GetTex("Tile3"));
            grid.create(max, max);
            grid.setNodePositions(max, max);
            grid.setupVisual();
            a = new AStarGridSearch(grid);

            Locator.Instance.getService<ICameraManager>().getCam().Pos = grid.getGrid[0, 0].Position + new Vector2(0, 100);

            Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<SteeringEntity>(Vector2.Zero);
            int id = Locator.Instance.getService<IEntityManager>().getEntity("Steering").UniqueID;
                
           IMind b = Locator.Instance.getService<IBehaviourManager>().getMind(id);
            test = b as SteeringMind;

            base.Initialize();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.key == Microsoft.Xna.Framework.Input.Keys.E)
            {
                if (Locator.Instance.getService<ICameraManager>().getCam().Zoom == 1f) {
                    Locator.Instance.getService<ICameraManager>().getCam().Zoom = 0.2f;
                } else if (Locator.Instance.getService<ICameraManager>().getCam().Zoom == 0.2f) {
                    Locator.Instance.getService<ICameraManager>().getCam().Zoom = 1f;
                }
            }



        }

        public override void Unload()
        {

            Locator.Instance.getService<KeyHandler>().KeyDown -= OnKeyDown;

            Locator.Instance.getService<MouseHandler>().MouseClick -= OnMouseClick;

            Locator.Instance.getService<IEntityManager>().tempCamClear();
            base.Unload();

        }

        private void doSearch()
        {
            for (int i = 0; i < max; i++)
            {

                a.addBlocked(5, i);
            }
            Node startnode = grid.getGrid[random.Next(1, max), random.Next(1, max)];
            Node endnode = grid.getGrid[random.Next(1, max), random.Next(1, max)];
            if (startnode.Blocked || endnode.Blocked)
            {
                doSearch();
            }
            else
            {
                grid.resetVisual();
                grid.path = null;
                search1.CommitPathSearch(grid, startnode, endnode);
                test.waypoints = search1.CommitWayPointSearch(grid, startnode, endnode);

            }

        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            doSearch();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            grid.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }



    }
}