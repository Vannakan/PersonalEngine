using Microsoft.Xna.Framework;
using Engine.States.Engine;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Entities;
using GameCode.Entities.Platformer;
using Engine.Interfaces.Collision;
using GameCode.Entities.Tiles;
using GameCode.Entities.Enemies.Boss;

namespace GameCode.Screens.Levels
{
    /// <summary>
    /// CLASS: This class is used for demonstrating the Separating Axis Theorem Collision Behaviour
    /// </summary>
    class SATDemo : BaseScreen
    {

        /// <summary>
        /// METHOD: Initialisation Logic in here
        /// </summary>
        public override void Initialize()
        {
            ///ADD: entities for testing the collision
            Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<pEntity>(Vector2.Zero);
            Locator.Instance.getService<IEntityManager>().createEntityDrawable<TileTest>(new Vector2(50, 50));
            Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<TestEntity>(new Vector2(200, 50));
            Locator.Instance.getService<IEntityManager>().createEntityCamDrawable<TestEntity>(new Vector2(150, 50));
            Locator.Instance.getService<IEntityManager>().createEntityDrawable<Boss1>(new Vector2(-100, -100));


            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {


           // System.Console.WriteLine(Locator.Instance.getService<IDetectionManager>().);

                base.Update(gameTime);
            }



        }
    }
