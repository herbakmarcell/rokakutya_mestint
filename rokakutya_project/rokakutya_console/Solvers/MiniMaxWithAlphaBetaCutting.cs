using rokakutya_console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rokakutya_console.Solvers
{
    public class MiniMaxWithAlphaBetaCutting : Solver
    {
        public int Depth { get; set; }

        public MiniMaxWithAlphaBetaCutting(OperatorGenerator operatorGenerator, int depth) : base(operatorGenerator)
        {
            Depth = depth;
        }


        public override State NextMove(State state)
        {
            Node currentNode = new Node(state);

            ExtendNode(currentNode, int.MinValue, int.MaxValue, currentNode.State.CurrentPlayer);

            return currentNode.Children[0].State;
        }

        private int ExtendNode(Node node, int alpha, int beta, char currentPlayerChar, bool currentPlayer = true)
        {
            if (node.State.GetStatus() != Status.PLAYING || node.Depth >= Depth) return node.GetHeuristics(currentPlayerChar);

            int v = currentPlayer ? int.MinValue : int.MaxValue;

            foreach (Operator op in Operators)
            {
                if (op.IsApplicable(node.State))
                {
                    State newState = op.Apply(node.State);
                    Node newNode = new Node(newState, node);
                    node.Children.Add(newNode);
                    node.SortChildrenMiniMax(currentPlayerChar, currentPlayer);

                    if (currentPlayer)
                    {
                        v = Math.Max(v, ExtendNode(newNode, alpha, beta, currentPlayerChar, !currentPlayer));
                        if (v > beta) return v;
                        alpha = Math.Max(alpha, v);
                    }
                    else
                    {
                        v = Math.Min(v, ExtendNode(newNode, alpha, beta, currentPlayerChar, !currentPlayer));
                        if (v < alpha) return v;
                        beta = Math.Min(beta, v);
                    }

                }
            }

            return v;
        }
    }
}
