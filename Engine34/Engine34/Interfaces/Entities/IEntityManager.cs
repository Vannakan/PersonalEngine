using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Engine.Interfaces.Entities
{
    /// <summary>
    /// INTERFACE: REsponsible for the implementation found in EntityManager which is responsible for adding and removing
    /// entities from the game scene
    /// </summary>
    public interface IEntityManager
    {
        /// <summary>
        /// METHOD: returns a list of entities
        /// </summary>
        /// <returns>List<IEntity></returns>
        List<IEntity> getList();

        /// <summary>
        /// METHOD: Returns a list of entities requiring a camera
        /// </summary>
        /// <returns>List of IEntity</returns>
        List<IEntity> getCamList();

        /// <summary>
        /// METHOD: Add an entity to the list of entities
        /// </summary>
        /// <param name="e">The entity to be added to the list</param>
        void addEntity(IEntity e);

        /// <summary>
        /// METHOD: Add an entity requiring a camera to the list of entities
        /// </summary>
        /// <param name="e">The entity to be added to the camera entity list</param>
        void addCamEntity(IEntity e);

        /// <summary>
        /// METHOD: Create a new IEntity and add it to the list of IEntities
        /// </summary>
        /// <typeparam name="T">A generic type of entity to be created</typeparam>
        /// <param name="Position">the position this entity will be initialised at</param>
        /// <returns></returns>
        IEntity createEntity<T>(Vector2 Position) where T : IEntity, new();

        /// <summary>
        /// METHOD: Create a new IEntity requiring a Camera and add it to the list of IEntites
        /// </summary>
        /// <typeparam name="T">The generic type of entity to be created</typeparam>
        /// <param name="Position">the position the entity will be initialised at</param>
        /// <returns></returns>
        IEntity createEntityCamDrawable<T>(Vector2 Position) where T : IEntity, new();

        /// <summary>
        /// METHOD: Create a new Drawable IEntity and add it to the list of IEntities
        /// </summary>
        /// <typeparam name="T">The generic type of entity to be created</typeparam>
        /// <param name="Position">the position the entity will be initialised at</param>
        /// <returns></returns>
        IEntity createEntityDrawable<T>(Vector2 Position) where T : IEntity, new();

        /// <summary>
        /// METHOD: Clear all Camera entities
        /// </summary>
        void tempCamClear();

        /// <summary>
        /// METHOD: Get an entity by it's name
        /// </summary>
        /// <param name="name">The name of the entity to be retrieved</param>
        IEntity getEntity(string name);

        /// <summary>
        /// METHOD: Get a Camera IEntity by it;s name
        /// </summary>
        /// <param name="name">The name of the entity to be returned</param>
        /// <returns>IEntity</returns>
        IEntity getCamEntity(string name);

        /// <summary>
        /// METHOD: Clear the list of all IEntities
        /// </summary>
        void clearList();

        /// <summary>
        /// METHOD: Remove a specific IEntity from the manager and list of Ientities
        /// </summary>
        /// <param name="entityID">The ID of the entity to be removed</param>
        void removeEntity(int entityID);

        /// <summary>
        /// METHOD: Remove a specific Camera entity by it;s ID
        /// </summary>
        /// <param name="entityID">the unique ID of the entity to be removed</param>
        void removeCamEntity(int entityID);

        /// <summary>
        /// METHOD: The Update loop that is called every frame
        /// </summary>
        /// <param name="gameTime">the monogame GameTime property</param>
        void Update(GameTime gameTime);

        void DestroyEnt(IEntity ent, int dens);

    }
}