public class ItemSpawner : Spawner<Item>
{
    public override void Create()
    {
        Item item = Instantiate(Template, transform);
        Deactivate(item);
    }

    public override void Activate()
    {
        base.Activate();

        ActivatingObject.gameObject.SetActive(true);
        ActivatingObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    public override void Deactivate(Item item)
    {
        base.Deactivate(item);
        item.gameObject.SetActive(false);
    }
}
