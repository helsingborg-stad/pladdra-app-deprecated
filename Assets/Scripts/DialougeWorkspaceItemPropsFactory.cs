using pladdra_app.Assets.Scripts.Data.Dialogs;

namespace pladdra_app.Assets.Scripts
{
    public class DialougeWorkspaceItemPropsFactory : IRenderItemsPropsFactory<DialogWorkspaceItem>
    {
        public RenderItemProps Create(DialogWorkspaceItem props)
        {
            return new RenderItemProps()
            {
                id = props.id,
                position = props.position,
                rotation = props.rotation,
                scale = props.scale,
                resource = new WorkspaceResource()
                {
                    id = props.id
                }
            };
        }
    }
}