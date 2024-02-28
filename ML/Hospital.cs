namespace ML
{
    public class Hospital
    {
        public int IdHospital { get; set; }

        public string Nombre  { get; set; }

        public string Direccion { get; set; }

        public DateTime AnioConstruccion { get; set; }

        public int Capacidad { get; set; }

        public ML.Especialidad Especialidad { get; set; }

        public List<ML.Hospital> Hospitales { get; set; }
    }
}
