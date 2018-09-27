using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pace.Server.Forms
{
    public partial class ViewImageForm : Form
    {
        private Image image;

        public ViewImageForm()
        {
            InitializeComponent();
        }

        public ViewImageForm(Image image) : this()
        {
            this.image = image;
        }

        private void ViewImageForm_Load(object sender, EventArgs e)
        {
            imagePictureBox.Image = image;
        }
    }
}