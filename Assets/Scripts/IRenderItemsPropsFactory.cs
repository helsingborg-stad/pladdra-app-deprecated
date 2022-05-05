namespace pladdra_app.Assets.Scripts
{
    public interface IRenderItemsPropsFactory<T>
    {
        RenderItemProps Create(T args);
    }
}