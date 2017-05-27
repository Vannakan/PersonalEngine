using Engine.Interfaces.Entities;
using Engine.Interfaces.ServiceLocator;
using Microsoft.Xna.Framework;

///<summary>INTERFACE:  The interface for the Behaviour Manager responsible for all Mind's controlling behaviour of entities
///</summary>
namespace Engine.Interfaces.Behaviour
{
    public interface IBehaviourManager : IUpdService
    {
        /// <summary>
        /// METHOD: Create a new IMind of type T
        /// </summary>
        /// <typeparam name="T">Generic type for the mind that will be created</typeparam>
        /// <param name="ie">the Entity the mind will be linked to</param>
        /// <returns>a brand new IMind</returns>
        IMind Create<T>(IEntity ie) where T : IMind, new();

        /// <summary>
        /// METHOD: Clears the current list of all minds
        /// </summary>
        void clearList();

        /// <summary>
        /// METHOD: Removes a specific mind from the list based on the id provided
        /// </summary>
        /// <param name="id">the unique ID of the mind to be removed</param>
        void removeMind(IMind mind);

        /// <summary>
        /// METHOD: Returns a mind based on the unique ID provided
        /// </summary>
        /// <param name="id">the Unique ID of the mind to be returned</param>
        /// <returns>the IMind associated with the id</returns>
        IMind getMind(int id);
        void Update(GameTime gameTime);

    }
}