using System;
using Grpc.Core;
using Helloworld;

namespace GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Channel channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
            var client1 = new MyTest.MyTestClient(channel);
            var t1 = client1.Test(new TestMsg() {Msg = "CSharp"});
            Console.WriteLine(t1.Msg);

            var clietn2 = new Greeter.GreeterClient(channel);
            var t2 = clietn2.SayHello(new HelloRequest() {Name = "lililili"});
            Console.WriteLine(t2.Message);

        }
    }
}
//  ./protoc.exe -I helloworld/ --csharp_out helloworld/Ts --grpc_out helloworld/Ts helloworld/test.proto --plugin=protoc-gen-grpc=./grpc_csharp_plugin.exe
//  ./protoc.exe -I helloworld/ --csharp_out helloworld/Ts --grpc_out helloworld/Ts helloworld/helloworld.proto --plugin=protoc-gen-grpc=./grpc_csharp_plugin.exe