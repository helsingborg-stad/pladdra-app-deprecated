using System;
using UnityEngine;
using UnityEngine.UIElements;
using UXHandlers;

namespace Workspace
{
    public interface IWorkspaceScene
    {
        GameObject Plane { get; }
        IWorkspaceObjectsManager ObjectsManager { get; }
        void UseHud(string templatePath, Action<VisualElement> bindUi);
        void UseUxHandler(IUxHandler handler);
        void ClearHud();
    }
}