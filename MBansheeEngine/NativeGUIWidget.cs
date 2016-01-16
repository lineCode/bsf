﻿using System;
using System.Runtime.CompilerServices;

namespace BansheeEngine
{
    /// <summary>
    /// Wrapper around the native GUIWidget class.
    /// <see cref="GUIWidget"/>
    /// </summary>
    internal class NativeGUIWidget : ScriptObject
    {
        private GUIPanel panel; // Populated by runtime

        /// <summary>
        /// Skin used for rendering all the GUI elements belonging to this widget.
        /// </summary>
        internal GUISkin Skin
        {
            set
            {
                IntPtr skinPtr = IntPtr.Zero;
                if (value != null)
                    skinPtr = value.GetCachedPtr();

                Internal_SetSkin(mCachedPtr, skinPtr);
            }
        }

        /// <summary>
        /// Determines to which camera are the GUI elements belonong to this widget rendered. If null then they will be
        /// rendered on the main camera.
        /// </summary>
        internal Camera Camera
        {
            set
            {
                IntPtr cameraPtr = IntPtr.Zero;
                if (value != null)
                    cameraPtr = value.Native.GetCachedPtr();

                Internal_SetCamera(mCachedPtr, cameraPtr);
            }
        }

        /// <summary>
        /// Container into which all GUI elements belonging to this widget should be placed.
        /// </summary>
        internal GUIPanel Panel
        {
            get { return panel; }
        }

        /// <summary>
        /// Creates a new native GUI widget and its wrapper.
        /// </summary>
        internal NativeGUIWidget()
        {
            Internal_Create(this);
        }

        /// <summary>
        /// Updates the transform of the GUI widget with the latest transform from the parent SceneObject.
        /// </summary>
        /// <param name="parentSO">Scene object the GUI widget component is attached to.</param>
        internal void UpdateTransform(SceneObject parentSO)
        {
            if (parentSO != null)
                Internal_UpdateTransform(mCachedPtr, parentSO.GetCachedPtr());
        }

        /// <summary>
        /// Updates the main camera, in case it changes. This is only relevant if the GUI widget is not rendering to a
        /// specific camera.
        /// </summary>
        /// <param name="camera">New main camera.</param>
        internal void UpdateMainCamera(Camera camera)
        {
            IntPtr cameraPtr = IntPtr.Zero;
            if (camera != null)
                cameraPtr = camera.Native.GetCachedPtr();

            Internal_UpdateMainCamera(mCachedPtr, cameraPtr);
        }

        /// <summary>
        /// Deletes the GUI widget and all child GUI elements.
        /// </summary>
        internal void Destroy()
        {
            Internal_Destroy(mCachedPtr);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_Create(NativeGUIWidget instance);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_UpdateTransform(IntPtr instance, IntPtr parentSO);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_UpdateMainCamera(IntPtr instance, IntPtr camera);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_SetSkin(IntPtr instance, IntPtr skin);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_SetCamera(IntPtr instance, IntPtr camera);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void Internal_Destroy(IntPtr instance);
    }
}
