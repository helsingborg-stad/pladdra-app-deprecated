namespace pladdra_app.Assets.Scripts
{
    public interface IDeserializable<T>
    {
        T Deserialize();
    }
}