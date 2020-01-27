﻿namespace ScriptableObjectArchitecture.Utility
{
    public static class SOArchitecture_Utility
    {
        public const int ASSET_MENU_ORDER_VARIABLES = 121;
        public const int ASSET_MENU_ORDER_EVENTS = 122;
        public const int ASSET_MENU_ORDER_COLLECTIONS = 123;

        public const string VARIABLE_SUBMENU = "Variables/";
        public const string COLLECTION_SUBMENU = "Collections/";
        public const string GAME_EVENT = "Game Events/";

        public const string ADVANCED_GAME_EVENT = GAME_EVENT + "Advanced/";
        public const string ADVANCED_VARIABLE_SUBMENU = VARIABLE_SUBMENU + "Advanced/";
        public const string ADVANCED_VARIABLE_COLLECTION = COLLECTION_SUBMENU + "Advanced/";

        // Add Component Menus
        public const string ADD_COMPONENT_ROOT_MENU = "SO Architecture/";
        public const string EVENTS_SUBMENU = ADD_COMPONENT_ROOT_MENU + "Events/";
        public const string EVENT_LISTENER_SUBMENU = EVENTS_SUBMENU + "Listeners/";
        public const string EVENT_RAISER_SUBMENU = EVENTS_SUBMENU + "Raisers/";
        public const string OBSERVER_SUBMENU = ADD_COMPONENT_ROOT_MENU + "Observers/";
    }
}