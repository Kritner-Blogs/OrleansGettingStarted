using System;

namespace Kritner.OrleansGettingStarted.Common.Config;

/// <summary>
/// Contains properties utilized for configuration Orleans
/// Clients and Cluster Nodes.
/// </summary>
public class OrleansConfig
{
    /// <summary>
    /// The IP addresses that will be utilized in the cluster.
    /// First IP address is the primary.
    /// </summary>
    public string[] NodeIpAddresses { get; set; }
    /// <summary>
    /// The port used for Client to Server communication.
    /// </summary>
    public int GatewayPort { get; set; }
    /// <summary>
    /// The port for Silo to Silo communication
    /// </summary>
    public int SiloPort { get; set; }
}
