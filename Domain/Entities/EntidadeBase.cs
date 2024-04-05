namespace Domain.Entities
{
    public abstract class EntidadeBase
    {

        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime Datacriacao { get; private set; } = DateTime.Now;
        public bool EmAtendimento { get; private set; } = false;

        public void MudarAtendimento()
        {
            if (EmAtendimento)
            {
                EmAtendimento = false;
            }
            else
            {
                EmAtendimento = true;
            }
        }
    }
}
