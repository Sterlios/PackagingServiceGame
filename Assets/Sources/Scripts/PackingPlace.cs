using UnityEngine;

public class PackingPlace : MonoBehaviour
{
    public Item Pack(Player player, PackingTable table, ActionAnimator actionAnimator)
    {
        player.transform.SetPositionAndRotation(transform.position, transform.rotation);
        actionAnimator.SetAnimatorParameter(ActionAnimator.PackingParameterHash);
        return table.Pack(player);
    }
}
