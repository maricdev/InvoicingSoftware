using Montesino_fakture.Model;
using System;
using System.Data;
using System.Linq;

namespace Montesino_fakture.Controller
{
    public class GetData
    {
        public DataTable GetTableSubjekti()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var subjekti = context.Subjekats.ToList();

                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Naziv2");
                    table.Columns.Add("jeKupac");
                    table.Columns.Add("jeDobavljac");
                    table.Columns.Add("OIB");
                    table.Columns.Add("Adresa");
                    table.Columns.Add("Posta_ID");
                    table.Columns.Add("Posta_Broj");
                    table.Columns.Add("Posta_Naziv");
                    table.Columns.Add("Telefon");
                    table.Columns.Add("Email");
                    table.Columns.Add("Drzava_ID");
                    table.Columns.Add("Drzava_Naziv");

                    foreach (Subjekat s in subjekti)
                    {
                        table.Rows.Add(s.Subjekat_ID, s.Naziv, s.Naziv2, s.jeKupac, s.jeDobavljac,
                            s.OIB, s.Adresa, s.Posta_ID, s.Posta.Broj.Trim() + " (" + s.Posta.Naziv + ")", s.Posta.Naziv,
                            s.Telefon, s.Email, s.Drzava_ID, s.Drzava.Naziv);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableKupci()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var subjekti = context.Subjekats.ToList();

                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Naziv2");
                    table.Columns.Add("jeKupac");
                    table.Columns.Add("jeDobavljac");
                    table.Columns.Add("OIB");
                    table.Columns.Add("Adresa");
                    table.Columns.Add("Posta_ID");
                    table.Columns.Add("Posta_Broj");
                    table.Columns.Add("Posta_Naziv");
                    table.Columns.Add("Telefon");
                    table.Columns.Add("Email");
                    table.Columns.Add("Drzava_ID");
                    table.Columns.Add("Drzava_Naziv");

                    foreach (Subjekat s in subjekti)
                    {
                        if (s.jeKupac.Trim() == "1")
                        {
                            table.Rows.Add(s.Subjekat_ID, s.Naziv, s.Naziv2, s.jeKupac, s.jeDobavljac,
                                s.OIB, s.Adresa, s.Posta_ID, s.Posta.Broj.Trim(), s.Posta.Naziv,
                                s.Telefon, s.Email, s.Drzava_ID, s.Drzava.Naziv);
                        }
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableArtikli(string prikaz)
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var artikli = context.Artikals.ToList();

                    table.Columns.Add("Artikal_ID");
                    table.Columns.Add("Aktivan");
                    table.Columns.Add("Artikal");
                    table.Columns.Add("Usluga");
                    table.Columns.Add("Sifra");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Opis");
                    table.Columns.Add("Cena");
                    table.Columns.Add("JM_ID");
                    table.Columns.Add("JM_Kod");
                    table.Columns.Add("PS_ID");
                    table.Columns.Add("PS_Naziv");

                    foreach (Artikal s in artikli)
                    {
                        if (prikaz.Trim().ToUpper() == "AKTIVNI")
                        {
                            if (s.Aktivan.ToString().Trim() == "1")
                                table.Rows.Add(s.Artikal_ID, s.Aktivan, s.Vrsta, s.Vrsta, s.Sifra, s.Naziv,
                            s.Opis, Formatiraj(Convert.ToDecimal(s.Cena)), s.JM.JM_ID, s.JM.Kod, s.Porez.PS_ID, s.Porez.Kod);
                        }
                        else
                            table.Rows.Add(s.Artikal_ID, s.Aktivan, s.Vrsta, s.Vrsta, s.Sifra, s.Naziv,
                                s.Opis, Formatiraj(Convert.ToDecimal(s.Cena)), s.JM.JM_ID, s.JM.Kod, s.Porez.PS_ID, s.Porez.Kod);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableJM()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var jediniceMere = context.JMs.ToList();

                    table.Columns.Add("JM_ID");
                    table.Columns.Add("Kod");
                    table.Columns.Add("Naziv");

                    foreach (Model.JM jm in jediniceMere)
                    {
                        table.Rows.Add(jm.JM_ID, jm.Kod, jm.Naziv);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTablePS()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var poreskeStope = context.Porezs.ToList();

                    table.Columns.Add("PS_ID");
                    table.Columns.Add("Kod");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Vrednost");
                    table.Columns["Vrednost"].DataType = System.Type.GetType("System.Decimal");

                    foreach (Porez jm in poreskeStope)
                    {
                        table.Rows.Add(jm.PS_ID, jm.Kod, jm.Naziv, Formatiraj(jm.Vrednost));
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableDrzava()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var drzave = context.Drzavas.ToList();

                    table.Columns.Add("Drzava_ID");
                    table.Columns.Add("Naziv");

                    foreach (Drzava jm in drzave)
                    {
                        table.Rows.Add(jm.Drzava_ID, jm.Naziv);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTablePoste()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var poste = context.Postas.ToList();

                    table.Columns.Add("Posta_ID");
                    table.Columns.Add("Broj");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Drzava_ID");

                    foreach (Posta jm in poste)
                    {
                        table.Rows.Add(jm.Posta_ID, jm.Broj, jm.Naziv, jm.Drzava_ID);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTablePosteByDrzavaID(int Drzava_ID)
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var poste = context.Postas.ToList();

                    table.Columns.Add("Posta_ID");
                    table.Columns.Add("Broj");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Drzava_ID");

                    foreach (Posta jm in poste)
                    {
                        if (Drzava_ID == jm.Drzava_ID)
                            table.Rows.Add(jm.Posta_ID, jm.Broj, jm.Naziv, jm.Drzava_ID);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableNP()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var nacini = context.NPs.ToList();

                    table.Columns.Add("NP_ID");
                    table.Columns.Add("Nacin");

                    foreach (Model.NP np in nacini)
                    {
                        table.Rows.Add(np.NP_ID, np.Nacin);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableOdgovorneOsobe()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var osobe = context.OdgovorneOsobes.ToList();

                    table.Columns.Add("OdgovorneOsobe_ID");
                    table.Columns.Add("Naziv");

                    foreach (Model.OdgovorneOsobe np in osobe)
                    {
                        table.Rows.Add(np.OdgovorneOsobe_ID, np.Naziv);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableValute()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var valute = context.Valutas.ToList();

                    table.Columns.Add("Valuta_ID");
                    table.Columns.Add("Oznaka");
                    table.Columns.Add("Naziv");

                    foreach (Model.Valuta np in valute)
                    {
                        table.Rows.Add(np.Valuta_ID, np.Oznaka, np.Naziv);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTablePodesavanja()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var pod = context.Podesavanjes.ToList();

                    table.Columns.Add("Podesavanje_ID");
                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Valuta_ID");
                    table.Columns.Add("NP_ID");
                    table.Columns.Add("OdgovorneOsobe_ID");
                    table.Columns.Add("RokVazenja");
                    table.Columns.Add("StatusPredracun_ID");
                    table.Columns.Add("StatusRacun_ID");

                    foreach (Model.Podesavanje np in pod)
                    {
                        table.Rows.Add(np.Podesavanje_ID, np.Subjekat_ID, np.Valuta_ID, np.NP_ID, np.OdgovorneOsobe_ID, np.RokVazenja, np.StatusPredracun_ID, np.StatusRacun_ID);
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableStatusi(string stanje)
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var statusi = context.Status.ToList();

                    table.Columns.Add("Status_ID");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("jePredracun");
                    table.Columns.Add("jeRacun");

                    foreach (Model.Status np in statusi)
                    {
                        Console.WriteLine(np.Naziv);
                        if (stanje.ToString().Trim() == "PON" && np.jePredracun.ToString().Trim() == "1")
                        {
                            table.Rows.Add(np.Status_ID, np.Naziv, np.jePredracun, np.jeRacun);
                            Console.WriteLine(np.Naziv);
                        }
                        else if (stanje.ToString().Trim() == "RAC" && np.jeRacun.ToString().Trim() == "1")
                        {
                            table.Rows.Add(np.Status_ID, np.Naziv, np.jePredracun, np.jeRacun);
                        }
                        else if (stanje.ToString().Trim() == "SVI")
                        {
                            table.Rows.Add(np.Status_ID, np.Naziv, np.jePredracun, np.jeRacun);
                        }
                    }
                    return table;
                }
            }
        }

        public DataTable GetTablePredracuni()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var predracuni = context.Predracuns.ToList();

                    table.Columns.Add("Predracun_ID");
                    table.Columns.Add("Broj");
                    table.Columns.Add("Datum");
                    table.Columns.Add("RokIsporuke");
                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Subjekat_Naziv");
                    table.Columns.Add("Primilac_ID");
                    table.Columns.Add("Primilac_Naziv");
                    table.Columns.Add("RokVazenja");
                    table.Columns.Add("NP_ID");
                    table.Columns.Add("Status_ID");
                    table.Columns.Add("Valuta_ID");
                    table.Columns.Add("OdgovorneOsobe_ID");
                    table.Columns.Add("Posta_ID");
                    table.Columns.Add("Napomena");
                    table.Columns.Add("Ukupno");
                    table.Columns.Add("PopustBroj");
                    table.Columns.Add("PopustProcenat");
                    table.Columns.Add("Vrednost");
                    table.Columns.Add("PDV");
                    table.Columns.Add("ZaPlacanje");
                    foreach (Model.Predracun np in predracuni)
                    {
                        table.Rows.Add(np.Predracun_ID, BrojRastavi(np.Broj), np.Datum.ToShortDateString(), np.RokIsporuke.ToShortDateString(),
                            np.Subjekat_ID, np.Subjekat.Naziv, np.Primalac_ID, np.Subjekat1.Naziv, np.RokVazenja, np.NP_ID, np.Status_ID,
                            np.Valuta_ID, np.OdgovorneOsobe_ID, np.Posta_ID, np.Napomena, Formatiraj(Convert.ToDecimal(np.Ukupno)),
                            Formatiraj(Convert.ToDecimal(np.PopustBroj)), Formatiraj(Convert.ToDecimal(np.PopustProcenat)),
                            Formatiraj(Convert.ToDecimal(np.Vrednost)), Formatiraj(Convert.ToDecimal(np.PDV)),
                            Formatiraj(Convert.ToDecimal(np.ZaPlacanje)));
                    }
                    return table;
                }
            }
        }

        public DataTable GetTablePredracuniStavke(string broj)
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var predracuni = context.PredracunStavkes.ToList();

                    table.Columns.Add("PredracunStavke_ID");
                    table.Columns.Add("Predracun_ID");
                    table.Columns.Add("Pozicija");
                    table.Columns.Add("Napomena");
                    table.Columns.Add("Artikal_ID");
                    table.Columns.Add("Sifra");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Opis");
                    table.Columns.Add("JM_ID");
                    table.Columns.Add("Kolicina");
                    table.Columns["Kolicina"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("Cena");
                    table.Columns["Cena"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("Rabat");
                    table.Columns["Rabat"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("Vrednost");
                    table.Columns["Vrednost"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("PS_ID");
                    table.Columns.Add("CenaPDV");
                    table.Columns["CenaPDV"].DataType = System.Type.GetType("System.Decimal");

                    foreach (Model.PredracunStavke np in predracuni)
                    {
                        if (broj.ToString().Trim() == np.Predracun_ID.ToString().Trim())
                            table.Rows.Add(np.PredracunStavke_ID, np.Predracun_ID, np.Pozicija, np.Napomena, np.Artikal_ID,
                                np.Sifra, np.Naziv, np.Opis, np.JM_ID, Formatiraj(np.Kolicina), Formatiraj(Convert.ToDecimal(np.Cena)),
                                Formatiraj(Convert.ToDecimal(np.Rabat)), Formatiraj(Convert.ToDecimal(np.Vrednost)), np.PS_ID, Formatiraj(Convert.ToDecimal(np.CenaPDV)));
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableRacuni(String tip)
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var racuni = context.Promets.ToList();

                    table.Columns.Add("Promet_ID");
                    table.Columns.Add("Predracun_ID");
                    table.Columns.Add("VrstaDokumenta");
                    table.Columns.Add("Broj");
                    table.Columns.Add("Otpremnica");
                    table.Columns.Add("Racun");
                    table.Columns.Add("PDVDate");
                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Subjekat_Naziv");
                    table.Columns.Add("Primilac_ID");
                    table.Columns.Add("Primilac_Naziv");
                    table.Columns.Add("RokVazenja");
                    table.Columns.Add("Dospece");
                    table.Columns.Add("NP_ID");
                    table.Columns.Add("Status_ID");
                    table.Columns.Add("Valuta_ID");
                    table.Columns.Add("OdgovorneOsobe_ID");
                    table.Columns.Add("Posta_ID");
                    table.Columns.Add("Napomena");
                    table.Columns.Add("Ukupno");
                    table.Columns.Add("PopustBroj");
                    table.Columns.Add("PopustProcenat");
                    table.Columns.Add("Vrednost");
                    table.Columns.Add("PDV");
                    table.Columns.Add("ZaPlacanje");
                    foreach (Model.Promet np in racuni.Where(a => a.VrstaDokumenta == tip))
                    {
                        table.Rows.Add(np.Promet_ID, np.Predracun_ID, np.VrstaDokumenta, BrojRastaviDokumenta(np.Broj), np.Otpremnica.ToShortDateString(), np.Racun.ToShortDateString(),
                            np.PDVDate.ToShortDateString(), np.Subjekat_ID, np.Subjekat.Naziv, np.Primalac_ID, np.Subjekat1.Naziv,
                            np.RokVazenja, np.Dospece.ToShortDateString(), np.NP_ID, np.Status_ID, np.Valuta_ID, np.OdgovorneOsobe_ID,
                            np.Posta_ID, np.Napomena, Formatiraj(Convert.ToDecimal(np.Ukupno)), Formatiraj(Convert.ToDecimal(np.PopustBroj)),
                            Formatiraj(Convert.ToDecimal(np.PopustProcenat)), Formatiraj(Convert.ToDecimal(np.Vrednost)),
                            Formatiraj(Convert.ToDecimal(np.PDV)), Formatiraj(Convert.ToDecimal(np.ZaPlacanje)));
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableRacuniStavke(string broj)
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var stavke = context.PrometStavkes.ToList();

                    table.Columns.Add("PrometStavke_ID");
                    table.Columns.Add("Promet_ID");
                    table.Columns.Add("Pozicija");
                    table.Columns.Add("Napomena");
                    table.Columns.Add("Artikal_ID");
                    table.Columns.Add("Sifra");
                    table.Columns.Add("Naziv");
                    table.Columns.Add("Opis");
                    table.Columns.Add("JM_ID");
                    table.Columns.Add("Kolicina");
                    table.Columns["Kolicina"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("Cena");
                    table.Columns["Cena"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("Rabat");
                    table.Columns["Rabat"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("Vrednost");
                    table.Columns["Vrednost"].DataType = System.Type.GetType("System.Decimal");
                    table.Columns.Add("PS_ID");
                    table.Columns.Add("CenaPDV");
                    table.Columns["CenaPDV"].DataType = System.Type.GetType("System.Decimal");

                    foreach (Model.PrometStavke np in stavke)
                    {
                        if (broj.ToString().Trim() == np.Promet_ID.ToString().Trim())
                            table.Rows.Add(np.PrometStavke_ID, np.Promet_ID, np.Pozicija, np.Napomena, np.Artikal_ID,
                                np.Sifra, np.Naziv, np.Opis, np.JM_ID, Formatiraj(np.Kolicina), Formatiraj(Convert.ToDecimal(np.Cena)),
                                Formatiraj(Convert.ToDecimal(np.Rabat)), Formatiraj(Convert.ToDecimal(np.Vrednost)), np.PS_ID, Formatiraj(Convert.ToDecimal(np.CenaPDV)));
                    }
                    return table;
                }
            }
        }

        public DataTable GetTableVezniDokument()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var predracuni = context.Predracuns.ToList();
                    table.Columns.Add("Predracun_ID");
                    table.Columns.Add("Broj");
                    table.Columns.Add("Datum");
                    table.Columns.Add("RokIsporuke");
                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Primilac_ID");
                    table.Columns.Add("RokVazenja");
                    table.Columns.Add("Vrednost");
                    table.Columns.Add("ZaPlacanje");
                    foreach (Model.Predracun np in predracuni)
                    {
                        table.Rows.Add(np.Predracun_ID, BrojRastavi(np.Broj), np.Datum.ToShortDateString(), np.RokIsporuke.ToShortDateString(),
                            np.Subjekat.Naziv, np.Subjekat1.Naziv, np.RokVazenja, Formatiraj(Convert.ToDecimal(np.Vrednost)), Formatiraj(Convert.ToDecimal(np.ZaPlacanje)));
                    }
                    return table;
                }
            }
        }
        public DataTable GetTableVezniDokumentZaOdobrenje()
        {
            using (MONTESINOEntities context = new MONTESINOEntities())
            {
                using (DataTable table = new DataTable())
                {
                    var racuni = context.Promets.ToList();

                    table.Columns.Add("Promet_ID");
                    table.Columns.Add("Broj");
                    table.Columns.Add("Otpremnica");
                    table.Columns.Add("Racun");
                    table.Columns.Add("Subjekat_ID");
                    table.Columns.Add("Primilac_ID");
                    table.Columns.Add("Vrednost");
                    table.Columns.Add("ZaPlacanje");

                    foreach (Model.Promet np in racuni.Where(a => a.VrstaDokumenta == "01"))
                    {
                        table.Rows.Add(np.Promet_ID, BrojRastaviDokumenta(np.Broj), np.Otpremnica.ToShortDateString(), np.Racun.ToShortDateString(),
                         np.Subjekat.Naziv, np.Subjekat1.Naziv, Formatiraj(Convert.ToDecimal(np.Vrednost)),  Formatiraj(Convert.ToDecimal(np.ZaPlacanje)));
                    }
                    return table;
                }
            }
        }
        public string BrojRastavi(String broj)
        {
            String rastavljen = broj.Insert(2, "-");
            return rastavljen.Insert(6, "-");
        }
        public string BrojRastaviDokumenta(String broj)
        {
            String rastavljen = broj.Insert(broj.Length - 2, "-");
            return rastavljen.Insert(broj.Length - 4, "-");
        }
        public string BrojSastavi(String broj)
        {
            return broj.Replace("-", "");
        }

        public string Formatiraj(decimal broj)
        {
            return string.Format("{0:#,##0.0000}", broj);
        }

        public string FormatirajProcenat(decimal broj)
        {
            return string.Format("{0:0.00}", broj);
        }

        public decimal IzracunajVrednost(decimal kolicina, decimal cena, decimal rabat)
        {
            return kolicina * cena * (100 - rabat) / 100;
        }

        public decimal IzracunajCenaPDV(decimal vrednost, decimal porez)
        {
            return vrednost * ((100 + porez) / 100);
        }

        //KONTRA RACUNANJE
        public decimal IzracunajOdVrednosti(decimal vrednost, decimal kolicina)
        {
            return vrednost / kolicina;
        }

        public decimal IzracunajOdCenaPDV(decimal CenaPDV, decimal PS)
        {
            return CenaPDV * (100 / (100 + PS));
        }

        private static String[] imebr = new String[] { "nula", "jedan", "dva", "tri", "četiri", "pet", "šest", "sedam", "osam", " devet" };

        public string Slovima(decimal Value, string valuta)
        {
            Value = Math.Round(Value, 2);
            if (Value == (decimal)0) return "nula dinara";
            bool NegativnaVrednost = (Value < 0);
            Value = Math.Abs(Value);
            String S = "";
            int celi = (int)Value;
            int dec = (int)(Math.Round(Value - celi, 2) * 100);
            String cbroj = celi.ToString("000000000000000");
            int I = 1;

            while (I < 15)
            {
                String tric = cbroj.Substring(I - 1, 3);
                int trojka = Convert.ToInt32(tric);
                if (tric != "000")
                {
                    String sl1 = "";

                    int cs = Convert.ToInt32(tric.Substring(0, 1));
                    int cd = Convert.ToInt32(tric.Substring(1, 1));
                    int cj = Convert.ToInt32(tric.Substring(2, 1));

                    if (cs == 2) S += "dve";
                    else if (cs > 2) S += imebr[cs];

                    if (cs == 1) S += "stotinu";
                    else if (cs == 2 || cs == 3 || cs == 4) S += "stotine";
                    else if (cs > 4) S += "stotina";

                    if (cj == 0) sl1 = ""; else sl1 = imebr[cj];

                    if (cd == 4) S += "četr";
                    else if (cd == 6) S += "šez";
                    else if (cd == 5) S += "pe";
                    else if (cd == 9) S += "deve";
                    else if (cd == 2 || cd == 3 || cd == 7 || cd == 8) S += imebr[cd];
                    else if (cd == 1)
                    {
                        sl1 = "";
                        if (cj == 0) S += "deset";
                        else if (cj == 1) S += "jeda";
                        else if (cj == 4) S += "četr";
                        else S += imebr[cj];
                        if (cj > 0) S += "naest";
                    }

                    if (cd > 1) S += "deset";

                    if ((I == 4 || I == 10) && cd != 1)
                    {
                        if (cj == 1) sl1 = "jedna";
                        else if (cj == 2) sl1 = "dve";
                    }

                    S += sl1;

                    if (I == 1)
                    {
                        S += "bilion";
                        if (cj > 1 || cd == 1) S += "a";
                    }
                    else if (I == 4)
                    {
                        S += "milijard";
                        if ((trojka % 100) > 11 && (trojka % 100) < 19) S += "i";
                        else if (cj == 1) S += "a";
                        else if (cj > 4 || cj == 0) S += "i";
                        else if (cj > 1) S += "e";
                    }
                    else if (I == 7)
                    {
                        S += "milion";
                        if (((trojka % 100) > 11 && (trojka % 100) < 19) || cj != 1) S += "a";
                    }
                    else if (I == 10)
                    {
                        S += "tisuć";
                        if (((trojka % 100) > 11 && (trojka % 100) < 19) || cj == 1) S += "a";
                        else if (trojka == 1) S += "u";
                        else if (cj > 4 || cj == 0) S += "a";
                        else if (cj > 1) S += "e";
                    }
                }
                I += 3;
            }
            if (dec > 0 && (valuta.Trim() != null || valuta.Trim() != "")) return S += " " + valuta + " " + dec.ToString() + "/100";
            else if (dec > 0 && (valuta.Trim() == null || valuta.Trim() == "")) return S += " " + dec.ToString() + "/100";
            else if ((valuta.Trim() != null || valuta.Trim() != "")) return S += " " + valuta;

            return S;
        }
    }
}