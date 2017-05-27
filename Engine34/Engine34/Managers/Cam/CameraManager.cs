using Engine.Interfaces.Cam;
using Engine.Managers.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Engine.Managers.Cam
{
    /// <summary>
    /// CLASS: A Manager for the camera, this is responsible for the creation of a Camera object and updating it in relation to
    /// the world position
    /// </summary>
    public class CameraManager : ICameraManager
    {
        //Container of the current cameras
        List<Camera> Cameras = new List<Camera>();
        ///DECLARE: reference to the main camera
        Camera MainCamera;


        /// <summary>
        /// METHOD: Creates a new camera
        /// </summary>
        public void Initialize()
        {
            MainCamera = new Camera();
        }

        /// <summary>
        /// METHOD: Returns the current Camera
        /// </summary>
        /// <returns>The current Camera</returns>
        public Camera getCam()
        {
            return MainCamera;
        }

        public Camera getCam(int id)
        {
            //for all cams in cam, if cam.id = id return cam else return null
            return null;
        }

        /// <summary>
        /// METHOD: Calls the update method of the Camera that is being managed
        /// </summary>
        /// <param name="gameTime">Monogame GameTime property</param>
        public void Update(GameTime gameTime)
        {
            MainCamera.Update(gameTime);
        }

        public void UpdateCameras(GameTime gameTime)
        {
            for (int i = 0; i < Cameras.Count; i++)
            {
                Cameras[i].Update(gameTime);
            }
        }

        //// <summary>
        //// METHOD: Returns a position relative to the world.
        //// 
        //// This can be used to retrieve the mouses current position 
        //// since mousestate returns the mouses x and y co-ords relative to the
        //// height and width and doesnt take the camera into consideration.
        //// </summary>
        //// <param name="Position">The position to be compared to the camera</param>
        //// <returns>Vector2 worldPosition</returns>
        public Vector2 getWorldPosition(Vector2 Position)
        {
            Vector2 worldPosition = Vector2.Transform(Position, Matrix.Invert(MainCamera.get_transformation(Constants.g)));

            return worldPosition;
        }



    }
}