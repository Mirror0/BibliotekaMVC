using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace WebApplication1.Models
{
    public class DateValitador:AbstractValidator<Wypozyczenia_Ksiazki>
    {
        public DateValitador()
        {
            RuleFor(x => x.Data_Zwrotu).GreaterThanOrEqualTo(x => x.Data_Wypozyczenia).WithMessage("Data Zwrotu jest przed Datą Wypożyczenia");
        }
    }

    public class FilmValidator : AbstractValidator<Wypozyczenia_Filmu>
    {
        public FilmValidator()
        {
            RuleFor(x => x.Data_Zwrotu).GreaterThanOrEqualTo(x => x.Data_Wypozyczenia).WithMessage("Data Zwrotu jest przed Datą Wypożyczenia");
        }
    }

    public class CzasopismoValidator : AbstractValidator<Wypozyczenia_Czasopisma>
    {
        public CzasopismoValidator()
        {
            RuleFor(x => x.Data_Zwrotu).GreaterThanOrEqualTo(x => x.Data_Wypozyczenia).WithMessage("Data Zwrotu jest przed Datą Wypożyczenia");
        }
    }

    public class PracaValidator : AbstractValidator<Wypozyczenia_Praca_Naukowa>
    {
        public PracaValidator()
        {
            RuleFor(x => x.Data_Zwrotu).GreaterThanOrEqualTo(x => x.Data_Wypozyczenia).WithMessage("Data Zwrotu jest przed Datą Wypożyczenia");
        }
    }

    public class RzeczValidator : AbstractValidator<Rzecz>
    {
        public RzeczValidator()
        {
            RuleFor(x => x.ilosc).GreaterThan(0).WithMessage("Ilość musi być większa niż 0");
            RuleFor(x => x.data_zwrotu).GreaterThanOrEqualTo(x => x.data_wypozyczenia).WithMessage("Data Zwrotu jest przed Datą Wypożyczenia");
        }
    }

    public class CzytelnikValidator : AbstractValidator<Czytelnik>
    {
        public CzytelnikValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Niepoprawny adres email");
            RuleFor(x => x.Uzytkownik).NotEmpty().WithMessage("Login jest wymagany");
            RuleFor(x => x.Haslo).NotEmpty().WithMessage("Hasło jest wymagane");
        }
    }

    public class LoginValidator : AbstractValidator<Czytelnik>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Wazne).Must(x => x == true).WithMessage("Twoje konto nie jest jeszcze aktywne");
        }
    }
}