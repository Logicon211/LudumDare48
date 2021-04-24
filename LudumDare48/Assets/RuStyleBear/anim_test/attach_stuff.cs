using UnityEngine;
using System.Collections;

public class attach_stuff : StateMachineBehaviour {


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (stateInfo.IsName("drink_votka"))
        {
            Transform tm = animator.gameObject.transform.Find("root_bear/bear__pelvis/bear__spine/bear__spine1/bear__l_clavicle/bear__l_upperarm/bear__l_forearm/bear__l_hand/InHandL_Bottle");
            ///Debug.Log("attach votka ");
            tm.gameObject.SetActive(true);
            return;
        }

        if (stateInfo.IsName("drink_votka0"))
        {
            Transform tm = animator.gameObject.transform.Find("root_bear/bear__pelvis/bear__spine/bear__spine1/bear__l_clavicle/bear__l_upperarm/bear__l_forearm/bear__l_hand/InHandL_Bottle");
            //Debug.Log("detach votka ");
            tm.gameObject.SetActive(false);
            return;
        }

        if (stateInfo.IsName("balalayka"))
        {
            Transform tm = animator.gameObject.transform.Find("root_bear/bear__pelvis/bear__spine/bear__spine1/bear__l_clavicle/bear__l_upperarm/bear__l_forearm/bear__l_hand/InHandL_Balalayka");
            tm.gameObject.SetActive(true);
            return;
        }

        if (stateInfo.IsName("balalayka2"))
        {
            Transform tm = animator.gameObject.transform.Find("root_bear/bear__pelvis/bear__spine/bear__spine1/bear__l_clavicle/bear__l_upperarm/bear__l_forearm/bear__l_hand/InHandL_Balalayka");
            tm.gameObject.SetActive(false);
            return;
        }

        if (stateInfo.IsName("ak_draw"))
        {
            Transform tm = animator.gameObject.transform.Find("root_bear/bear__pelvis/bear__spine/bear__spine1/bear__r_clavicle/bear__r_upperarm/bear__r_forearm/bear__r_hand/InHandR_Gun");
            tm.gameObject.SetActive(true);
            return;
        }

        if (stateInfo.IsName("ak_holster"))
        {
            Transform tm = animator.gameObject.transform.Find("root_bear/bear__pelvis/bear__spine/bear__spine1/bear__r_clavicle/bear__r_upperarm/bear__r_forearm/bear__r_hand/InHandR_Gun");
            tm.gameObject.SetActive(false);
            return;
        }
    }


}
