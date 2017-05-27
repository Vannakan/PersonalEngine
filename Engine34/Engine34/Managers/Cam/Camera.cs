using Engine.Interfaces.Entities;
using Engine.Interfaces.InputManager;
using Engine.Managers.Input;
using Engine.Managers.ServiceLocator;
using Engine34.Managers.Cam;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Engine.Managers.Cam
{
    /// <summary>
    /// CLASS: The camera that is used and manipulated to display the viewport
    /// The camera can have 4 different states each affecting it's behaviour
    /// </summary>
    public enum CameraType
    {
        Static,
        Locked,
        fPlayer,
        fMouse
    }
    public class Camera
    {
        ///Used to determine the cameras behaviour
        public CameraType cType;
        bool Follow = false, Locked = false;
        bool isPossessed = false;
        protected IEntity p; ///Target Entity
        protected float _zoom; /// Camera Zoom
        public Matrix _transform; /// Matrix Transform
        public Vector2 _pos; /// Camera Position
        protected float _rotation; /// Camera Rotation
        protected Vector2 _oPos; ///Original position;

        Vector2 Force;

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public Camera()
        {
            ///Initalise the properties of the camera
            _zoom = 0.7f;
            _rotation = 0f;
            _pos = Vector2.Zero;
        }


        /// <summary>
        /// METHOD: Sets and gets zoom
        /// </summary>
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f) _zoom = 0.1f;

                if (_zoom > 1f)
     _zoom = 1f;
            } /// Negative zoom will flip image
        }

        /// <summary>
        /// GET: SET: Rotation
        /// </summary>
        public float Rotation
        {
            ///Return the rotation and set it to the value of this property
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }

        /// <summary>
        /// METHOD: Move the camera
        /// </summary>
        /// <param name="amount">Vector2 to move the camera by</param>
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        /// <summary>
        /// GET: SET: Position
        /// </summary>
        public Vector2 Pos
        {
            get
            {
                return _pos;
            }
            set
            {
                _pos = value;
                _oPos = value;
            }
        }

        //// <summary>
        //// METHOD: Sets an entity of interest for the camera
        //// Also sets the type of behaviour we wish our camera to use with this entity
        //// </summary>
        //// <param name="e">Entity to be set as interest</param>
        //// <param name="Type">The type of behaviour</param>
        public void setEntity(IEntity e, CameraState state)
        {
            p = e;
            isPossessed = true;
            switch (state)
            {
                case CameraState.FOLLOW:
                    Follow = true;
                    Locked = false;
                    break;
                case CameraState.LOCKED:
                    Locked = true;
                    Follow = false;
                    break;
                case CameraState.DEFAULT:                  
                        Follow = false;
                        Locked = false;
                        setPos(Vector2.Zero);
                    break;
                    

            }
        }

        /// <summary>
        /// METHOD: Returns a matrix of the current transformation of the camera
        /// </summary>
        /// <param name="graphicsDevice">Monogame GraphicsDevice</param>
        /// <returns>Matrix _transform</returns>
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform = /// Thanks to o KB o for this solution
            Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
            Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0));
            return _transform;
        }

        //// <summary>
        //// METHOD: Check if the camera is being possessed and if so, then check for any move requests
        //// </summary>
        public void Update(GameTime gt)
        {
            checkPossession(gt);
        }

        /// <summary>
        /// METHOD: Used to check if the camera is posessed by an object so that any move requests can be completed
        /// </summary>
        public void checkPossession(GameTime gameTime)
        {
            Force = Vector2.Zero;
            if (isPossessed)
            {

                if (Follow)
                    FollowPossessed(gameTime);

                if (Locked)
                    LockOnPossessed();
            }
        }

       private void FollowPossessed(GameTime gameTime)
        {
            Force = p.Velocity;
            Pos += Force / (int)gameTime.ElapsedGameTime.TotalMilliseconds * 16 ;

        }

        private void LockOnPossessed()
        {
            _pos = p.Position;

        }

        /// <summary>
        /// METHOD: Move the camera to a set position
        /// </summary>
        /// <param name="pos">Position to be set to</param>
        public void setPos(Vector2 pos)
        {
            _pos = pos;
        }

        //// <summary>
        //// METHOD : Reset the camera position to 0,0
        //// </summary>
        public void reset()
        {
            isPossessed = false;
            _pos = Vector2.Zero;
            _zoom = 0.7f;

        }

        //// <summary>
        //// METHOD: Check for input
        //// </summary>
        public void checkInput()
        {
            ///If the L key is pressed then the camera is moved to match the zoom
            if (Locator.Instance.getService<IInputManager>().CheckHeldDown(Keys.L))
            {
                if (Zoom >= 0.5f)
                    _pos.X += 4;

                if (Zoom <= 0.5f)
                    _pos.X += 14;
            }
            ///If J is pressed then the camera is moved again
            if (Locator.Instance.getService<IInputManager>().CheckHeldDown(Keys.J))
            {
                if (Zoom >= 0.5f)
                    _pos.X -= 4;

                if (Zoom <= 0.5f)
                    _pos.X -= 14;
            }
            ///If I is pressed the camera is moved on the Y axis to match the zoom
            if (Locator.Instance.getService<IInputManager>().CheckHeldDown(Keys.I))
            {
                if (Zoom >= 0.5f)
                    _pos.Y -= 4;

                if (Zoom <= 0.5f)
                    _pos.Y -= 14;
            }
            ///Same as above but with K
            if (Locator.Instance.getService<IInputManager>().CheckHeldDown(Keys.K))
            {
                if (Zoom >= 0.5f)
                    _pos.Y += 4;

                if (Zoom <= 0.5f)
                    _pos.Y += 14;
            }
        }

    }
}