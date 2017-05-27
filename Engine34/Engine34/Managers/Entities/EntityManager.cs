using Engine.Entities;
using Engine.Managers.Behaviour;
using Engine.Managers.Render;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Engine.Managers.Cam;
using Engine.Interfaces.Entities;
using Engine.Interfaces.ServiceLocator;
using Engine.Managers.ServiceLocator;
using Engine.Interfaces.Render;
using Engine.Interfaces.Cam;
using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Collision;
using Engine.Managers.Collision;
using Engine.Interfaces.Screen;

namespace Engine.Managers.Entities
{
    /// <summary>
    /// CLASS: Entity Manager is responsible for the creation and storage of all entities in the game scene. It is able to
    /// return a list, a specific entity, or create and remove entities from the scene
    /// </summary>
    public class EntityManager : IEntityManager, IUpdService
    {
        ///DECLARE: A reference to the Camera
        Camera cam = Locator.Instance.getService<ICameraManager>().getCam();
        ///DECLARE: A list of all IEntities in the scene
        private List<IEntity> eList = new List<IEntity>();
        ///DECLARE: A list of Camera entities in the scene
        private List<IEntity> cdList = new List<IEntity>();

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public EntityManager()
        {

        }

        //// <summary>
        //// METHOD: Returns all entities
        //// </summary>
        //// <returns>A list of all current entities</returns>
        public List<IEntity> getList()
        {
            return eList;
        }

        //// <summary>
        //// METHOD: Returns all Cam draw Entities
        //// </summary>
        //// <returns>A list of all current entities using the camera</returns>
        public List<IEntity> getCamList()
        {
            return cdList;
        }

        //// <summary>
        //// METHOD: Add an entity to the entity list
        //// </summary>
        //// <param name="e">The entity to be added to the list</param>
        public void addEntity(IEntity e)
        {

            eList.Add(e);
            Locator.Instance.getService<IRenderManager>().addDrawable(e as IDrawableComponent);
        }

        //// <summary>
        //// METHOD: Add an entity to the camdraw list
        //// </summary>
        //// <param name="e">The entity to be added to the list</param>
        public void addCamEntity(IEntity e)
        {
            cdList.Add(e);

        }

        /// <summary>
        /// METHOD: Create a new entity and add it to the entity List
        /// </summary>
        /// <typeparam name="T">The generic IEntity object to be created</typeparam>
        /// <param name="Position">The position to create the object at</param>
        /// <returns></returns>
        public IEntity createEntity<T>(Vector2 Position) where T : IEntity, new()
        {
            IEntity a = new T();
            a.Initialize(Position);
            addEntity(a);
            return a;
        }
        /// <summary>
        /// METHOD: Create a new Camera entity and add it to the camera entity list
        /// </summary>
        /// <typeparam name="T">The generic IEntity object to be created</typeparam>
        /// <param name="Position">The position to create the object at</param>
        /// <returns></returns>
        public IEntity createEntityCamDrawable<T>(Vector2 Position) where T : IEntity, new()
        {
            IEntity a = new T();
            a.Initialize(Position);
            addCamEntity(a);

            return a;
        }

        /// <summary>
        /// METHOD: Create a new Drawable entity and add it to the Camera list
        /// </summary>
        /// <typeparam name="T">The generic IEntity object to be created</typeparam>
        /// <param name="Position">The position to create the object at</param>
        /// <returns></returns>
        public IEntity createEntityDrawable<T>(Vector2 Position) where T : IEntity, new()
        {
            IEntity a = new T();
            a.Initialize(Position);

            Locator.Instance.getService<IRenderManager>().addCamDrawEntity(a as IDrawableComponent);
            return a;
        }

        /// <summary>
        /// METHOD: Clear all camera entities
        /// </summary>
        public void tempCamClear()
        {
            Locator.Instance.getService<IRenderManager>().clearTempEntity();
            Locator.Instance.getService<IBehaviourManager>().clearList();

        }

        //// <summary>
        //// METHOD: Return a reference to an entity based on their name
        //// </summary>
        //// <param name="name">The name of the entity to be found</param>
        public IEntity getEntity(string name)
        {
            IEntity e = null;
            for (int i = 0; i < cdList.Count; i++)
            {
                string naem = cdList[i].Name;
                if (naem == name)
                {
                    e = cdList[i];
                }
            }

            return e;
        }

        //// <summary>
        //// METHOD: Return a reference to a Camera Draw entity based on their name
        //// </summary>
        //// <param name="name">The name of the entity to be returned</param>
        //// <returns>An entity with a name matching the parameter</returns>
        public IEntity getCamEntity(string name)
        {
            IEntity e = null;
            for (int i = 0; i < cdList.Count; i++)
            {
                string naem = cdList[i].Name;
                if (naem == name)
                {
                    e = cdList[i];
                }
            }
            return e;
        }

        //// <summary>
        //// METHOD: Clear the non cam draw entity list
        //// </summary>
        public void clearList()
        {
           
            eList.Clear();
            cdList.Clear();
            Locator.Instance.getService<IBehaviourManager>().clearList();
        }


        //// <summary>
        //// METHOD: Remove an entity based on the ID applied
        //// </summary>
        //// <param name="entityID">The ID of the entity to be removed</param>
        public void removeEntity(int entityID)
        {
            for (int i = 0; i < eList.Count; i++)
            {
                if (eList[i].UniqueID == entityID)
                {
                    eList.Remove(eList[i]);
                    Locator.Instance.getService<IBehaviourManager>().removeMind(eList[i].Mind);
                }
            }
        }


        //// <summary>
        //// METHOD: Remove a Cam Draw entity based on the ID applied
        //// </summary>
        //// <param name="entityID">the unique ID o the entity to be removed</param>
        public void removeCamEntity(int entityID)
        {
            for (int i = 0; i < cdList.Count; i++)
            {
                if (cdList[i].UniqueID == entityID)
                {
                    cdList.Remove(cdList[i]);
                    Locator.Instance.getService<IBehaviourManager>().removeMind(cdList[i].Mind);
                }
            }
        }

        //// <summary>
        //// METHOD: The update loop which is cycled through every frame
        //// </summary>
        //// <param name="gameTime">Monogame GameTime property</param>
        public void Update(GameTime gameTime)
        {
            ///IF: An entity goes wildly off screen
            for (int i = 0; i < eList.Count; i++)
            {
                if (eList[i].Position.X < -100 || eList[i].Position.Y > 1000)
                {
                    ///Remove the entity
                    removeEntity(eList[i].UniqueID);
                }
            }
        }

        public void DestroyEnt(IEntity ent, int dens)
        {
                Locator.Instance.getService<IDetectionManager>().RemoveCollision((ICollidable)ent.Mind);
                Locator.Instance.getService<IRenderManager>().Destroy(ent);
                Locator.Instance.getService<IBehaviourManager>().removeMind(ent.Mind);
                eList.Remove(ent);
                cdList.Remove(ent);
                ent.Destroy();
                ent = null;
        }
    }
}