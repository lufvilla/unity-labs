using System.Linq;
using Demos.MovingFloor;
using UnityEngine;

public class FloorComputeShaderRunner : Floor
{
    public ComputeShader computeShader;
    
    private struct FloorData
    {
        public Vector3 Position;
        public float Value;
        public static int Size => sizeof(float) + (sizeof(float) * 3);
    };
    
    private struct TargetData
    {
        public Vector3 Position;
        public static int Size => sizeof(float) * 3;
    };
        
    private TargetData[] _targetsData = {};
    private FloorData[] _floorsData = {};

    private ComputeBuffer _targetsBuffer;
    private ComputeBuffer _floorsBuffer;

    protected override void Start()
    {
        base.Start();
        
        _floorsData = Floors.Select(x => new FloorData {Position = x.Child.transform.position}).ToArray();
    }

    protected override void UpdateFloor()
    {
        _targetsData = Targets.Select(x => new TargetData {Position = x.position}).ToArray();
        if (_targetsData.Length > 0)
        {
            _targetsBuffer = new ComputeBuffer(_targetsData.Length, TargetData.Size);
            _targetsBuffer.SetData(_targetsData);
            computeShader.SetBuffer(0, "Targets", _targetsBuffer);
        }
        
        if (_floorsData.Length > 0)
        {
            _floorsBuffer = new ComputeBuffer(_floorsData.Length, FloorData.Size);
            _floorsBuffer.SetData(_floorsData);
            computeShader.SetBuffer(0, "Floors", _floorsBuffer);
        }

        computeShader.SetFloat("maxValue", Config.Distance.MaxValue);
        computeShader.SetFloat("minValue", Config.Distance.MinValue);
        computeShader.SetFloat("normalizedValue", Config.Distance.MaxValue - Config.Distance.MinValue);
        computeShader.Dispatch(0, _floorsData.Length, 1, 1);
        
        _floorsBuffer.GetData(_floorsData);
        
        for (int i = 0; i < _floorsData.Length; i++)
            Floors[i].SetValue(_floorsData[i].Value);
            
        _floorsBuffer?.Dispose();
        _targetsBuffer?.Dispose();
    }
}
