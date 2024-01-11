namespace Repository
{
    /// <summary>
    /// Хранит данные, которые в рантайме модифицируют какие-то аспекты игры
    /// </summary>
    public class ModifiersRepository
    {
        /// <summary>
        /// Модификатор значений шаров, которые спавнятся в бросателе.
        /// </summary>
        public int ValueMod { get; set; }
    }
}