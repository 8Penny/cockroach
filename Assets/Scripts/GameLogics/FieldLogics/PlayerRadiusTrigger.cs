using System;
using Services.Game;
using UnityEngine;
using Views;

namespace GameLogics.FieldLogics
{
    public class PlayerRadiusTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag(EntityTag.Cockroach))
            {
                return;
            }
            if (col.TryGetComponent<CockroachView>(out var view))
            {
                view.EnterDangerZone();
            }
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.CompareTag(EntityTag.Cockroach))
            {
                return;
            }
            if (col.TryGetComponent<CockroachView>(out var view))
            {
                view.ExitDangerZone();
            }
        }
    }
}