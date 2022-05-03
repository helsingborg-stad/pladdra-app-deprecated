namespace pladdra_app.Assets.Scripts.ExampleScreens
{
    public class WelcomeScreen : Screen
    {
         private void Start() {
            GetComponentInParent<ScreenManager>().SetActiveScreen<LoadProjectsScreen>();
         }
   }
}