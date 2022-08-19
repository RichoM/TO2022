using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Profiling
{
    public partial class TallyResults : Form
    {
        private Tally tally;
        private TallyGraph graph;

        public TallyResults()
        {
            InitializeComponent();
        }

        public TallyResults (Tally tally)
        {
            this.tally = tally;
            InitializeComponent();
        }

        private void TallyResults_Load(object sender, EventArgs e)
        {
            tally.WriteToFile();

            graph = new TallyGraph(tally);
            graph.Extent = scene.World.Extent;
            scene.World.AddChild(graph);

            timer1.Enabled = true;
        }

        private void scene_Resize(object sender, EventArgs e)
        {
            if (graph != null)
            {
                graph.Extent = scene.World.Extent;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (graph != null)
            {
                Text = string.Format("Frame: {0}, Update: {1} ms, Draw: {2} ms, Instances: {3}",
                    graph.Selected,
                    tally.DrawDurations.ElementAtOrDefault(graph.Selected),
                    tally.UpdateDurations.ElementAtOrDefault(graph.Selected),
                    tally.InstanceCounter.ElementAtOrDefault(graph.Selected));
                if (graph.Width < tally.Count * 6)
                {
                    scene.Width = tally.Count * 6;
                }
            }
        }
    }
}
