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
        
        /// <summary>
        /// Значение шара, который будет спавниться в бросателе (не будет рандома)
        /// </summary>
        public int FixValue { get; set; }
        
        /// <summary>
        /// Зафиксировать ли значение шара, который спавниться в бросателе (выключить рандом)
        /// </summary>
        public bool IsFixValue { get; set; }
    }
}