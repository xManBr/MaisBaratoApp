using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace Mercoplano.Maisbarato.Server.RESTful
{
    public class Config
    {
        public static string IMG_EM_PROCESSAMENTO = "Imagem em processamento";
        public static string BIG_LABEL = "BIG";
        public static string SMALL_LABEL = "SMALL";
        public static string IMAGE_PATH_1 = "ProductPicture";
        public static int DAYS_AGO = 30;// Até quantos dias atraz vai pegar os precos para mostrar na pesquisa
        public static decimal Radius = 1;// 1 grau equivale a um raio de 111,12 km Raio (coordenadas geograficas) padrão para procura de lojas...
        public static string DEF = "DEF";
        public static string SENT = "SENT";
        public static String PEND = "PEND";
        public static String ACT = "ACT";
        public static Char DefaultSeparator = '-';
        public static String EMAIL_AUTHENTICATIONINFO = "";
        public static int DEAD_LINE_MINUTE_TOKEN = 30;
        public static String UrlComplaint = "/api/Complaint/Anonymous";

    }
}