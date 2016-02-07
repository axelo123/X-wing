namespace Outils
{
    partial class SelecteurMoisAnnee
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlpPrincipal = new System.Windows.Forms.TableLayoutPanel();
            this.buttonMoisPrecedent = new System.Windows.Forms.Button();
            this.comboMois = new System.Windows.Forms.ComboBox();
            this.comboAnnee = new System.Windows.Forms.ComboBox();
            this.buttonMoisSuivant = new System.Windows.Forms.Button();
            this.tooltipAideContextuelle = new System.Windows.Forms.ToolTip(this.components);
            this.tlpPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpPrincipal
            // 
            this.tlpPrincipal.AutoSize = true;
            this.tlpPrincipal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpPrincipal.ColumnCount = 4;
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPrincipal.Controls.Add(this.buttonMoisPrecedent, 0, 0);
            this.tlpPrincipal.Controls.Add(this.comboMois, 1, 0);
            this.tlpPrincipal.Controls.Add(this.comboAnnee, 2, 0);
            this.tlpPrincipal.Controls.Add(this.buttonMoisSuivant, 3, 0);
            this.tlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.tlpPrincipal.Margin = new System.Windows.Forms.Padding(0);
            this.tlpPrincipal.Name = "tlpPrincipal";
            this.tlpPrincipal.RowCount = 1;
            this.tlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPrincipal.Size = new System.Drawing.Size(671, 35);
            this.tlpPrincipal.TabIndex = 0;
            // 
            // buttonMoisPrecedent
            // 
            this.buttonMoisPrecedent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMoisPrecedent.Location = new System.Drawing.Point(19, 6);
            this.buttonMoisPrecedent.Margin = new System.Windows.Forms.Padding(19, 6, 19, 6);
            this.buttonMoisPrecedent.Name = "buttonMoisPrecedent";
            this.buttonMoisPrecedent.Size = new System.Drawing.Size(48, 23);
            this.buttonMoisPrecedent.TabIndex = 0;
            this.buttonMoisPrecedent.Text = "<<";
            this.tooltipAideContextuelle.SetToolTip(this.buttonMoisPrecedent, "Permet de passer au mois précédent");
            this.buttonMoisPrecedent.UseVisualStyleBackColor = true;
            this.buttonMoisPrecedent.Click += new System.EventHandler(this.buttonMoisPrecedent_Click);
            // 
            // comboMois
            // 
            this.comboMois.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboMois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMois.FormattingEnabled = true;
            this.comboMois.Location = new System.Drawing.Point(105, 6);
            this.comboMois.Margin = new System.Windows.Forms.Padding(19, 6, 19, 6);
            this.comboMois.Name = "comboMois";
            this.comboMois.Size = new System.Drawing.Size(211, 21);
            this.comboMois.TabIndex = 1;
            this.tooltipAideContextuelle.SetToolTip(this.comboMois, "Permet de choisir le mois à afficher");
            this.comboMois.SelectedIndexChanged += new System.EventHandler(this.comboMois_SelectedIndexChanged);
            // 
            // comboAnnee
            // 
            this.comboAnnee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboAnnee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAnnee.FormattingEnabled = true;
            this.comboAnnee.Location = new System.Drawing.Point(354, 6);
            this.comboAnnee.Margin = new System.Windows.Forms.Padding(19, 6, 19, 6);
            this.comboAnnee.Name = "comboAnnee";
            this.comboAnnee.Size = new System.Drawing.Size(211, 21);
            this.comboAnnee.TabIndex = 2;
            this.tooltipAideContextuelle.SetToolTip(this.comboAnnee, "Permet de choisir l\'année à afficher");
            this.comboAnnee.SelectedIndexChanged += new System.EventHandler(this.comboAnnee_SelectedIndexChanged);
            // 
            // buttonMoisSuivant
            // 
            this.buttonMoisSuivant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonMoisSuivant.Location = new System.Drawing.Point(603, 6);
            this.buttonMoisSuivant.Margin = new System.Windows.Forms.Padding(19, 6, 19, 6);
            this.buttonMoisSuivant.Name = "buttonMoisSuivant";
            this.buttonMoisSuivant.Size = new System.Drawing.Size(49, 23);
            this.buttonMoisSuivant.TabIndex = 3;
            this.buttonMoisSuivant.Text = ">>";
            this.tooltipAideContextuelle.SetToolTip(this.buttonMoisSuivant, "Permet de passer au mois suivant");
            this.buttonMoisSuivant.UseVisualStyleBackColor = true;
            this.buttonMoisSuivant.Click += new System.EventHandler(this.buttonMoisSuivant_Click);
            // 
            // SelecteurMoisAnnee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tlpPrincipal);
            this.Name = "SelecteurMoisAnnee";
            this.Size = new System.Drawing.Size(671, 35);
            this.tlpPrincipal.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpPrincipal;
        private System.Windows.Forms.Button buttonMoisPrecedent;
        private System.Windows.Forms.ToolTip tooltipAideContextuelle;
        private System.Windows.Forms.ComboBox comboMois;
        private System.Windows.Forms.ComboBox comboAnnee;
        private System.Windows.Forms.Button buttonMoisSuivant;
    }
}
