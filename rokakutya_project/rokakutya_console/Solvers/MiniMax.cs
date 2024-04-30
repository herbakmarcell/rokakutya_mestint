using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.Solvers
{
    public class MiniMax : Solver
    {
        public int Depth { get; set; }

        public MiniMax(OperatorGenerator operatorGenerator, int depth) : base(operatorGenerator)
        {
            Depth = depth;
        }

        public override State NextMove(State state)
        {
            Node currentNode = new Node(state);
            ExtendNode(currentNode);

            currentNode.SortChildrenMiniMax(state.CurrentPlayer);

            return currentNode.Children[0].State;

        }

        private void ExtendNode(Node node)
        {
            if (node.State.GetStatus() != Status.PLAYING || node.Depth >= Depth) return;
            foreach (Operator o in Operators)
            {
                if (o.IsApplicable(node.State))
                {
                    State newState = o.Apply(node.State);
                    Node newNode = new Node(newState, node);
                    node.Children.Add(newNode);
                    ExtendNode(newNode);
                }
            }
        }
    }
}
