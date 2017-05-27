using Engine.Events.CollisionEvent;
using Engine.Interfaces.ServiceLocator;
using Engine.Managers;
using Engine.Managers.Collision;
using Engine34.Interfaces.Collision;
using Microsoft.Xna.Framework;


namespace Engine.Interfaces.Collision
{
    /// <summary>
    ///INTERFACE: An interface for the collision detection manager
    /// </summary>
    public interface IDetectionManager : IUpdService
    {
        void ClearCollisionList();

        /// <summary>
        /// METHOD: Adds an ICOllidable to the list of objects to check
        /// </summary>
        /// <param name="obj">The object to add to the list</param>
        void addCollidable(ICollidable obj);

        void addMapCollidable(Engine34.Grid.CollisionNode obj);

        void resetCollisionLayer();
        /// <summary>
        /// METHOD: The update loop which is cycled through every frame
        /// </summary>
        /// <param name="gameTime">Monogame GameTime property</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// METHOD: Checks AABB Collision for two objects
        /// </summary>
        void doCollision();

        /// <summary>
        /// METHOD: When two objects are colliding via AABB collision, this method is called
        /// </summary>
        /// <param name="A">The first object that's colliding</param>
        /// <param name="B">The second object that's colliding</param>
        void OnACollision(ICollidable A, ICollidable B);

        /// <summary>
        /// METHOD: When two objects are calculated to be colliding with the SAT logic, this method is called and moves them
        /// apart
        /// </summary>
        /// <param name="A">The first object that's colliding</param>
        /// <param name="B">The second object that's colliding</param>
        /// <param name="mtv">The minimum translation vector calculated in the SAT Loop</param>
        void CallSAT(ICollidable A, ICollidable B, Vector2 mtv);

        /// <summary>
        /// METHOD: Checks for whether or not two objects are colliding according to the SAT Logic
        /// </summary>
        /// <param name="a">The first object that's colliding</param>
        /// <param name="b">The second object that's colliding</param>
        void CheckSAT(ICollidable a, ICollidable b);

        /// <summary>
        /// METHOD: Creates the impulse to be applied to an entity when it is colliding allowing it to simulate physics
        /// when it reacts
        /// </summary>
        /// <param name="a">The first object that's colliding</param>
        /// <param name="b">The second object that's colliding</param>
        /// <returns>A vector2 which is applied to the objects</returns>
        Vector2 ImpulseApplication(IHitbox a, IHitbox b);


        void RemoveCollision(ICollidable hit);

        event CollisionHandler.CollisionEventHandler OnSATCollision;
        event CollisionHandler.CollisionEventHandler OnCollision;

    }
}