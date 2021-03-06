using UnityEngine;

namespace pladdra_app.Assets.Scripts
{
    public abstract class Screen: MonoBehaviour {
        virtual public object[] GetScreenChildObjects() { return new object[0]; }
        public void SetScreenActive (bool active) {
            gameObject.SetActive(active);

            foreach(var child in GetScreenChildObjects()) {
                var childScreen = child as Screen;
                if (childScreen) {
                    childScreen.SetScreenActive(active);
                    continue;
                }
                var childMonoBehaviour = child as MonoBehaviour;
                if (childMonoBehaviour) {
                    childMonoBehaviour.gameObject.SetActive(active);
                    continue;
                }
                var childGameObject = child as GameObject;
                if (childGameObject) {
                    childGameObject.SetActive(active);
                    continue;
                }
            }
        }
    }
}