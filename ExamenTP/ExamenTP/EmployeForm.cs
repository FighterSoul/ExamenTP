using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ExamenTP
{
    public class EmployeForm : Form
    {
        private List<Employe> employes = new List<Employe>();

        private GroupBox groupBoxInfo;
        private Label labelNom;
        private TextBox textBoxNom;
        private Label labelPoste;
        private ComboBox comboBoxPoste;
        private Label labelSalaire;
        private TextBox textBoxSalaire;
        private Label labelDateEmbauche;
        private DateTimePicker dateTimePickerEmbauche;

        private GroupBox groupBoxRecherche;
        private ComboBox comboBoxCritere;
        private TextBox textBoxCritere1;
        private TextBox textBoxCritere2;
        private Button buttonRechercher;

        private Button buttonAjouter;
        private Button buttonModifier;
        private Button buttonSupprimer;
        private Button buttonAfficherTout;
        private Button buttonQuitter;
        private ListBox listBoxEmployes;

        private Employe employeSelectionne = null;

        public EmployeForm()
        {
            this.Text = "Gestion d'Employe";
            this.Size = new System.Drawing.Size(700, 600);

            groupBoxInfo = new GroupBox
            {
                Text = "Informations Employe",
                Size = new System.Drawing.Size(350, 200),
                Location = new System.Drawing.Point(10, 10)
            };

            labelNom = new Label
            {
                Text = "Nom de l'employe",
                Location = new System.Drawing.Point(10, 30)
            };

            textBoxNom = new TextBox
            {
                Location = new System.Drawing.Point(150, 30),
                Size = new System.Drawing.Size(180, 20)
            };

            labelPoste = new Label
            {
                Text = "Poste",
                Location = new System.Drawing.Point(10, 60)
            };

            comboBoxPoste = new ComboBox
            {
                Location = new System.Drawing.Point(150, 60),
                Size = new System.Drawing.Size(180, 20)
            };
            comboBoxPoste.Items.AddRange(new string[] { "Ouvrier", "Technicien", "Ingenieur", "Autre" });

            labelSalaire = new Label
            {
                Text = "Salaire",
                Location = new System.Drawing.Point(10, 90)
            };

            textBoxSalaire = new TextBox
            {
                Location = new System.Drawing.Point(150, 90),
                Size = new System.Drawing.Size(180, 20)
            };

            labelDateEmbauche = new Label
            {
                Text = "Date d'embauche",
                Location = new System.Drawing.Point(10, 120)
            };

            dateTimePickerEmbauche = new DateTimePicker
            {
                Location = new System.Drawing.Point(150, 120),
                Format = DateTimePickerFormat.Short
            };

            groupBoxInfo.Controls.Add(labelNom);
            groupBoxInfo.Controls.Add(textBoxNom);
            groupBoxInfo.Controls.Add(labelPoste);
            groupBoxInfo.Controls.Add(comboBoxPoste);
            groupBoxInfo.Controls.Add(labelSalaire);
            groupBoxInfo.Controls.Add(textBoxSalaire);
            groupBoxInfo.Controls.Add(labelDateEmbauche);
            groupBoxInfo.Controls.Add(dateTimePickerEmbauche);

            groupBoxRecherche = new GroupBox
            {
                Text = "Recherche",
                Size = new System.Drawing.Size(350, 150),
                Location = new System.Drawing.Point(10, 220)
            };

            comboBoxCritere = new ComboBox
            {
                Location = new System.Drawing.Point(10, 30),
                Size = new System.Drawing.Size(150, 20)
            };
            comboBoxCritere.Items.AddRange(new string[] { "Nom", "Poste", "Plage de salaire" });
            comboBoxCritere.SelectedIndexChanged += ComboBoxCritere_SelectedIndexChanged;

            textBoxCritere1 = new TextBox
            {
                Location = new System.Drawing.Point(180, 30),
                Size = new System.Drawing.Size(150, 20),
                Visible = true
            };

            textBoxCritere2 = new TextBox
            {
                Location = new System.Drawing.Point(180, 60),
                Size = new System.Drawing.Size(150, 20),
                Visible = false
            };

            buttonRechercher = new Button
            {
                Text = "Rechercher",
                Location = new System.Drawing.Point(10, 100),
                Size = new System.Drawing.Size(150, 30)
            };
            buttonRechercher.Click += ButtonRechercher_Click;

            groupBoxRecherche.Controls.Add(comboBoxCritere);
            groupBoxRecherche.Controls.Add(textBoxCritere1);
            groupBoxRecherche.Controls.Add(textBoxCritere2);
            groupBoxRecherche.Controls.Add(buttonRechercher);

            
            buttonAjouter = new Button
            {
                Text = "Ajouter",
                Location = new System.Drawing.Point(380, 30),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonAjouter.Click += ButtonAjouter_Click;

            buttonModifier = new Button
            {
                Text = "Modifier",
                Location = new System.Drawing.Point(380, 70),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonModifier.Click += ButtonModifier_Click;

            buttonSupprimer = new Button
            {
                Text = "Supprimer",
                Location = new System.Drawing.Point(380, 110),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonSupprimer.Click += ButtonSupprimer_Click;

            buttonAfficherTout = new Button
            {
                Text = "Afficher Tout",
                Location = new System.Drawing.Point(380, 150),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonAfficherTout.Click += ButtonAfficherTout_Click;

            buttonQuitter = new Button
            {
                Text = "Quitter",
                Location = new System.Drawing.Point(380, 190),
                Size = new System.Drawing.Size(120, 30)
            };
            buttonQuitter.Click += (sender, e) => this.Close();

            listBoxEmployes = new ListBox
            {
                Location = new System.Drawing.Point(10, 400),
                Size = new System.Drawing.Size(650, 150)
            };
            listBoxEmployes.SelectedIndexChanged += ListBoxEmployes_SelectedIndexChanged;

            this.Controls.Add(groupBoxInfo);
            this.Controls.Add(groupBoxRecherche);
            this.Controls.Add(buttonAjouter);
            this.Controls.Add(buttonModifier);
            this.Controls.Add(buttonSupprimer);
            this.Controls.Add(buttonAfficherTout);
            this.Controls.Add(buttonQuitter);
            this.Controls.Add(listBoxEmployes);
        }

        private void ListBoxEmployes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxEmployes.SelectedIndex != -1)
            {
                employeSelectionne = employes[listBoxEmployes.SelectedIndex];
                textBoxNom.Text = employeSelectionne.Nom;
                comboBoxPoste.SelectedItem = employeSelectionne.Poste;
                textBoxSalaire.Text = employeSelectionne.Salaire.ToString();
                dateTimePickerEmbauche.Value = employeSelectionne.DateEmbauche;
            }
        }

        private void ComboBoxCritere_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCritere.SelectedItem.ToString() == "Plage de salaire")
            {
                textBoxCritere2.Visible = true;
                textBoxCritere1.Text = "Salaire Min";
                textBoxCritere2.Text = "Salaire Max";
            }
            else
            {
                textBoxCritere2.Visible = false;
                textBoxCritere1.Text = "Entrez le critère";
            }
        }

        private bool ValiderSalaire(string salaire)
        {
            if (string.IsNullOrWhiteSpace(salaire))
            {
                MessageBox.Show("Le salaire ne peut pas être vide.");
                return false;
            }

            if (!Regex.IsMatch(salaire, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Le salaire doit être un nombre valide (avec max 2 décimales).");
                return false;
            }

            decimal salaireParse;
            if (!decimal.TryParse(salaire, out salaireParse) || salaireParse < 0)
            {
                MessageBox.Show("Le salaire doit être un nombre positif.");
                return false;
            }

            return true;
        }

        private void ButtonAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxNom.Text))
                {
                    MessageBox.Show("Le nom de l'employé est obligatoire.");
                    return;
                }

                if (comboBoxPoste.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez sélectionner un poste.");
                    return;
                }

                if (!ValiderSalaire(textBoxSalaire.Text))
                {
                    return;
                }

                string nom = textBoxNom.Text;
                string poste = comboBoxPoste.SelectedItem.ToString();
                decimal salaire = decimal.Parse(textBoxSalaire.Text);
                DateTime dateEmbauche = dateTimePickerEmbauche.Value;

                Employe newEmploye = new Employe(nom, poste, salaire, dateEmbauche);
                employes.Add(newEmploye);

                MessageBox.Show("Employé ajouté avec succès !");
                ClearForm();
                DisplayEmployes(employes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout de l'employé : " + ex.Message);
            }
        }

        private void ButtonModifier_Click(object sender, EventArgs e)
        {
            if (employeSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un employé à modifier.");
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(textBoxNom.Text))
                {
                    MessageBox.Show("Le nom de l'employé est obligatoire.");
                    return;
                }

                if (comboBoxPoste.SelectedIndex == -1)
                {
                    MessageBox.Show("Veuillez sélectionner un poste.");
                    return;
                }

                if (!ValiderSalaire(textBoxSalaire.Text))
                {
                    return;
                }

                employeSelectionne.Nom = textBoxNom.Text;
                employeSelectionne.Poste = comboBoxPoste.SelectedItem.ToString();
                employeSelectionne.Salaire = decimal.Parse(textBoxSalaire.Text);
                employeSelectionne.DateEmbauche = dateTimePickerEmbauche.Value;

                MessageBox.Show("Employé modifié avec succès !");
                DisplayEmployes(employes);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification de l'employé : " + ex.Message);
            }
        }

        private void ButtonSupprimer_Click(object sender, EventArgs e)
        {
            if (employeSelectionne == null)
            {
                MessageBox.Show("Veuillez sélectionner un employé à supprimer.");
                return;
            }

            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet employé ?",
                "Confirmation de suppression",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                employes.Remove(employeSelectionne);
                MessageBox.Show("Employé supprimé avec succès !");
                DisplayEmployes(employes);
                ClearForm();
                employeSelectionne = null;
            }
        }

        private void ButtonAfficherTout_Click(object sender, EventArgs e)
        {
            DisplayEmployes(employes);
        }

        private void ButtonRechercher_Click(object sender, EventArgs e)
        {
            if (comboBoxCritere.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner un critère de recherche.");
                return;
            }

            string critere = comboBoxCritere.SelectedItem.ToString();
            List<Employe> resultats;

            if (critere == "Nom")
            {
                resultats = employes.Where(emp => emp.Nom.IndexOf(textBoxCritere1.Text, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else if (critere == "Poste")
            {
                resultats = employes.Where(emp => emp.Poste.Equals(textBoxCritere1.Text, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            else 
            {
                if (decimal.TryParse(textBoxCritere1.Text, out decimal min) && decimal.TryParse(textBoxCritere2.Text, out decimal max))
                {
                    resultats = employes.Where(emp => emp.Salaire >= min && emp.Salaire <= max).ToList();
                }
                else
                {
                    MessageBox.Show("Veuillez entrer des valeurs valides pour la plage de salaire.");
                    return;
                }
            }

            DisplayEmployes(resultats);
        }

        private void DisplayEmployes(List<Employe> employeList)
        {
            listBoxEmployes.Items.Clear();
            foreach (var employe in employeList)
            {
                listBoxEmployes.Items.Add(employe.ToString());
            }
        }

        private void ClearForm()
        {
            textBoxNom.Clear();
            comboBoxPoste.SelectedIndex = -1;
            textBoxSalaire.Clear();
            dateTimePickerEmbauche.Value = DateTime.Now;
            employeSelectionne = null;
        }
    }
}