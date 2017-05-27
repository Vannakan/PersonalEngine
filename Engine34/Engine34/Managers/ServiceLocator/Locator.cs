using Engine.Events.KeyboardEvent;
using Engine.Events.MouseEvent;
using Engine.Interfaces.Behaviour;
using Engine.Interfaces.Cam;
using Engine.Interfaces.Collision;
using Engine.Interfaces.Entities;
using Engine.Interfaces.InputManager;
using Engine.Interfaces.Render;
using Engine.Interfaces.Resource;
using Engine.Interfaces.Screen;
using Engine.Interfaces.ServiceLocator;
using Engine.Interfaces.Sound;
using Engine.Managers.Behaviour;
using Engine.Managers.Cam;
using Engine.Managers.Collision;
using Engine.Managers.Entities;
using Engine.Managers.Input;
using Engine.Managers.Render;
using Engine.Managers.Resource;
using Engine.Managers.Sound;
using Engine34.Interfaces.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Engine.Managers.ServiceLocator
{
    /// <summary>
    /// CLASS: A service locator that removes the need for each manager to be a singleton. This is now the only
    /// Singleton required in the whole program. It allows us to access each manager and call the method from each
    /// Manager as we require it
    /// </summary>
    public class Locator : GameComponent, IServiceLocator
    {

        ///DECLARE: Dictionary of all available services
        private Dictionary<object, object> _services = new Dictionary<object, object>();
        ///DECLARE: Dictionary of all services that need to be updated
        private Dictionary<object, object> _updservices = new Dictionary<object, object>();

        /// DECLARE: List of Managers needing Updating
        private List<IUpdService> updateList = new List<IUpdService>();
        ///DECLARE: Private instance
        private static Locator instance;

        private static Game _game1;

        public Game _Game1 { get { return _game1; } set { _game1 = value; } }

        /// <summary>
        /// CONSTRUCTOR:
        /// </summary>
        private Locator(Game game) : base(game)
        {
            _game1 = game;
        }

        /// <summary>
        /// SINGLETON
        /// </summary>
        public static Locator Instance
        {
            get
            {
                if (instance == null)
                    instance = new Locator(_game1);
                return instance;
            }
        }

        /// <summary>
        /// METHOD: Initialises each service for the program to run
        /// </summary>
        /// <param name="c">the monogame ContentManager to be used</param>
        /// <param name="sb">the monogame spriteBatch to be used</param>
        public void InitializeServices(ContentManager c, SpriteBatch sb)
        {
            ///Mouse and Keyboard Handlers
            MouseHandler mouseHandle = new MouseHandler();
            registerUPDService<MouseHandler>(mouseHandle);
            KeyHandler keyHandle = new KeyHandler();
            registerUPDService<KeyHandler>(keyHandle);

            ///Behaviour Manager
            IInputManager inputManage = new InputManager();
            registerUPDService<IInputManager>(inputManage);

            ///Camera Manager
            ICameraManager camManage = new CameraManager();
            camManage.Initialize();
            registerUPDService<ICameraManager>(camManage);

            ///sound Manager
            ISoundManager soundManage = new SoundManager();
            registerService<ISoundManager>(soundManage);

            ///Resource Loader
            IResourceLoader resource = new ResourceLoader();
            resource.Content = c;
            resource.Initialize();
            registerService<IResourceLoader>(resource);

            ///Entity Manager
            IEntityManager entityManage = new EntityManager();
            registerService<IEntityManager>(entityManage);

            ///Behaviour Manager
            IBehaviourManager behaveManage = new BehaviourManager();
            registerUPDService<IBehaviourManager>(behaveManage);

            ///Collision Manager
            IDetectionManager detectManage = new DetectionManager();
            registerUPDService<IDetectionManager>(detectManage);

            ///Render Manager
            IRenderManager render = new RenderManager();
            registerUPDService<IRenderManager>(render);

            registerService<ILightMaskManager>(render as ILightMaskManager);

            IAnimationManager animManager = new Engine34.Managers.Animation.AnimationManager();
            animManager.Initialize();
            registerUPDService<IAnimationManager>(animManager);

            ///Screen Manager
            IScreenManager screenManage = new ScreenManager();
            screenManage.Initialize();
            registerService<IScreenManager>(screenManage);
            soundManage.Initialize();

            registerUPDService<IScreenManager>(screenManage);




        }

        /// <summary>
        /// METHOD: Gets and returns a service of the generic type input in the parameter
        /// </summary>
        /// <typeparam name="T">Generic type T of the service to be requested</typeparam>
        /// <returns>Service of Type T</returns>
        public T getService<T>()
        {
            T t =
             default(T);
            ///TRY: to return the service from the hashmap
            try
            {
                if (_services.ContainsKey(typeof(T)) && _services[typeof(T)] != null)
                    t = (T)_services[typeof(T)];
            } ///TRY: to catch a null reference exception
            catch (NullReferenceException)
            {
                throw new Exception("SERVICE DOESNT EXIST OR HASNT BEEN INITIALIZED");

            }

            return t;
        }

        /// <summary>
        /// METHOD: Register the service and check whether it needs to be updated
        /// </summary>
        /// <typeparam name="T">Generic service</typeparam>
        /// <param name="val">Service of Type T</param>
        public void registerService<T>(T val)
        {
            ///TRY:
            try
            {
                ///IF: val parameter is not null and is in the _services Dictionary
                if (val != null && !_services.ContainsKey(typeof(T)))
                {
                    ///ADD: Service to the dictionary
                    _services.Add(typeof(T), val);
                }
            }
            ///CATCH: Exception
            catch (KeyNotFoundException)
            {
                ///THROW: Argument exception
                throw new ArgumentException("ISNT VALID");
            }
        }

        /// <summary>
        /// METHOD: Register Update service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        public void registerUPDService<T>(T val)
        {
            registerService<T>(val);

            IUpdService val1 = val as IUpdService;
            updateList.Add(val1);

        }

        /// <summary>
        /// METHOD: Update all services that require it
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateServices(GameTime gameTime)
        {

            foreach (IUpdService a in updateList)
            {
                if (a != null)
                    a.Update(gameTime);
            }
        }
    }
}