# Unity与SQL的连接

> 实现了Unity与MySQL的数据传输
>
> unity源码: https://github.com/hmxsqaq/Unity-MySQL

![image-20230411025642894](https://hmxs-1315810738.cos.ap-shanghai.myqcloud.com/img/202304110256047.png)

## 各Button作用

- **Connect:** 实例化SqlHelper类，调用SqlHelper类中的ConnectSql函数与对应数据库进行连接。若连接成功则自动调用一次SqlHelper类中的GetData()函数，获取数据；并Debug”连接成功!”,改变右侧UI，使其变为“连接成功”，同时使下方功能按钮变得可用。
- **UpdateData:** 调用一次SqlHelper类中的GetData()函数，主要用于在数据库的数据被改变后进行数据更新。
- **DebugData:** 在控制台按行顺次输出数据。
- **ShowData:** 在右侧UI中显示数据。右侧UI为一个Grid Group，当ShowData被点击时，其会根据数据数量实例化相应数量的Data预制体，并根据数据为每个Data预制体下的Text组件赋值，以达到表单可视化的目的。
- **InserData:** 读取右侧Input框中用户输入的数据，并将其插入数据库中

![image-20230411030959092](https://hmxs-1315810738.cos.ap-shanghai.myqcloud.com/img/202304110309157.png)