using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Managers.Collision;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

///<summary> A Manager that is responsible for the creation of the Mind for each entity and stores a list of all minds
/// with unique IDs.
///</summary>
namespace Engine.Managers.Behaviour
{
    public class BehaviourManager : IBehaviourManager
    {


        ///List for all of the AI to update and check
        private List<IMind> minds = new List<IMind>();


        //// <summary>
        //// Creates and returns an IMind interface
        //// </summary>
        //// <typeparam name="T"></typeparam>
        //// <param name="ie">The entity to be linked to the created mind</param>
        //// <returns>A new mind that is created</returns>
        public IMind Create<T>(IEntity ie) where T : IMind, new()
        {
            ///Create a new mind
            IMind e = new T();

            ///Link the mind to the entity it controls
            e.Link(ie);

            ///Add the mind to the collision List in the Collision manager.
            Locator.Instance.getService<IDetectionManager>().addCollidable(e.getCollidable());

            ///Add the mind to the list of minds.
            minds.Add(e);

            ///Return the mind
            return e;

        }



        //// <summary>
        //// METHOD: Clears the current Mind List
        //// </summary>

        public void clearList()
        {
            ///For every mind in the list
            for (int i = 0; i < minds.Count; i++)
            {
                ///Remove each mind
                minds[i].Unload();
                removeMind(minds[i]);
            }
            ///Clear the list to ensure the garbage collector gets everything
            minds.Clear();
        }

        //// <summary>
        //// METHOD: Removes a specific mind from the list via its ID. 
        //// </summary>
        //// <param name="id">The Unique ID of the mind to be removed</param>
        public void removeMind(IMind mind)
        {
            if(mind != null)
            mind.Unload();
            minds.Remove(mind);
        }

        /// <summary>
        /// METHOD: Finds the mind in the list of the id provided
        /// </summary>
        /// <param name="id">the unique ID of the mind to be found</param>
        /// <returns>an IMind with the same unique ID</returns>
        public IMind getMind(int id)
        {
            ///Every mind in the list of minds
            for (int i = 0; i < minds.Count; i++)
            {
                ///If the id of this mind matches the parameter
                if (minds[i].UniqueID == id)
                {
                    ///Return this mind
                    return minds[i];

                }
            }
            ///If no mind has an ID matching we can return null
            return null;
        }

        //// <summary>
        //// METHOD: Iterate through the mind list and call the minds update
        //// </summary>
        //// <param name="gameTime">Monogame GameTime property</param>
        public void Update(GameTime gameTime)
        {
            ///For every mind in the list, run the Update method of each mind 
            for (int i = 0; i < minds.Count; i++)
            {

                if (minds[i].Active)
                    minds[i].Update(gameTime);
            }
        }
    }
}