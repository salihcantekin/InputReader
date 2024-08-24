namespace InputReader.InputReaders.Queue.QueueItems;
internal class QueueItemsOrder
{
    /* ############## STEPS #############

          - Write Message (Opt)             -> (PrintProcessor)
          - Read RawValue                   -> (IInputReaderBase)
          - PreValidators

          - AllowedValue Check (Opt) (rawValue)
          - Use ValueConverter.Convert (ValueConverter)
          - InRange AllowedValue Check (Opt) (Converted Type) (OPT)
    */

    public const int ProcessPrintQueueItem = 1;
    public const int ConsoleReadLineQueueItem = 2;
    public const int PreValidatorQueueItem = 3;
    public const int AllowedValuesCheckQueueItem = 4;
    public const int ValueConverterQueueItem = 5;
    public const int InRangeAllowedValueQueueItem = 6;
    public const int CreateInstanceQueueItem = int.MaxValue;
}
