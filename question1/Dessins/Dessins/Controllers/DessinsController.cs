using Dessins.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dessins.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DessinsController : ControllerBase
    {
        
        [HttpGet]
        // Rien à modifier ici, juste un exemple de dessin très simple
        public ActionResult GetDrawing1()
        {
            var drawSquare = new DrawSquare(2, 2);
            
            return Ok(drawSquare);
        }
        // TODO: Il faut ajouter une nouvelle action pour dessiner la séquence mentionnée dans l'énoncé

        public ActionResult GetDrawing2() {


            var drawCircle = new DrawCircle(1,1);
            var wait1 = new Wait(3);
            var carreGauche = new DrawSquare(0, 2);
            var carreDroit = new DrawSquare(2, 2);
            var wait2 = new Wait(1);
            var etoile = new DrawStar(1,3,20);


            drawCircle.DrawingEvents = [];
            drawCircle.DrawingEvents.Add(wait1);
            wait1.DrawingEvents = [];
            wait1.DrawingEvents.Add(carreGauche);
            wait1.DrawingEvents.Add(carreDroit);
            carreGauche.DrawingEvents = [];
            carreGauche.DrawingEvents.Add(wait2);
            wait2.DrawingEvents = [];
            wait2.DrawingEvents.Add(etoile);

            return Ok(drawCircle);
        
        }
    }
}
