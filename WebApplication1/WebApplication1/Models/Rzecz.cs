using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Rzecz
    {
        private Ksiazka ksiazka;
        private Film film;
        private Czasopismo czasopismo;
        private Praca_Naukowa pracaNaukowa;
        public int ilosc;

        public Rzecz(Ksiazka k,int ilosc)
        {
            ksiazka = k;
            this.ilosc = ilosc;
            film = null;
            czasopismo = null;
            pracaNaukowa = null;
        }

        public Rzecz(Film f, int ilosc)
        {
            ksiazka = null;
            this.ilosc = ilosc;
            film = f;
            czasopismo = null;
            pracaNaukowa = null;
        }

        public Rzecz(Czasopismo cz, int ilosc)
        {
            ksiazka = null;
            this.ilosc = ilosc;
            film = null;
            czasopismo = cz;
            pracaNaukowa = null;
        }

        public Rzecz(Praca_Naukowa pn, int ilosc)
        {
            ksiazka = null;
            this.ilosc = ilosc;
            film = null;
            czasopismo = null;
            pracaNaukowa = pn;
        }

        public Object getRzecz()
        {
            if (ksiazka != null)
            {
                return ksiazka;
            }
            else if (film != null)
            {
                return film;
            }
            else if (czasopismo != null)
            {
                return czasopismo;
            }
            else if (pracaNaukowa != null)
            {
                return pracaNaukowa;
            }

            return null;
        }

    }
}