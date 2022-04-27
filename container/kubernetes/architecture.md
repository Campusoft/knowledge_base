# kubernetes - architecture

A running Kubernetes cluster contains node agents (kubelet) and a cluster control plane (AKA master), with cluster state backed by a distributed storage system (etcd).

The Kubernetes control plane is split into a set of components, which can all run on a single master node, or can be replicated in order to support high-availability clusters, or can even be run on Kubernetes itself (AKA self-hosted).


- Kubernetes Design and Architecture
- Identifiers and Names in Kubernetes
- Design Principles
- Kubernetes Architectural Roadmap
https://github.com/kubernetes/community/tree/master/contributors/design-proposals/architecture


# Controllers

In Kubernetes, controllers are control loops that watch the state of your cluster, then make or request changes where needed. Each controller tries to move the current cluster state closer to the desired state.

Controller pattern
A controller tracks at least one Kubernetes resource type. These objects have a spec field that represents the desired state. The controller(s) for that resource are responsible for making the current state come closer to that desired state.


Job is a Kubernetes resource that runs a Pod, or perhaps several Pods, to carry out a task and then stop.

# Scheduler

In Kubernetes, scheduling refers to making sure that Pods are matched to Nodes so that Kubelet can run them.

kube-scheduler

kube-scheduler is the default scheduler for Kubernetes and runs as part of the control plane. kube-scheduler is designed so that, if you want and need to, you can write your own scheduling component and use that instead.

For every newly created pod or other unscheduled pods, kube-scheduler selects an optimal node for them to run on. However, every container in pods has different requirements for resources and every pod also has different requirements. Therefore, existing nodes need to be filtered according to the specific scheduling requirements.


https://kubernetes.io/docs/concepts/scheduling-eviction/kube-scheduler/

# Pod

Kubernetes makes a bolder step and chooses a group of cohesive containers, called a Pod, as the smallest deployable unit.

Pods are the smallest deployable units of computing that you can create and manage in Kubernetes.

Pod networking

Each Pod is assigned a unique IP address for each address family. Every container in a Pod shares the network namespace, including the IP address and network ports

Within a Pod, containers share an IP address and port space, and can find each other via localhost.


Pods in a Kubernetes cluster are used in two main ways:
Pods that run a single container. 
Pods that run multiple containers that need to work together.

## Multi Container Pods In Kubernetes

Multi Container Pods In Kubernetes
- Design-patterns of Multi Container Pods. (Ilustraciones)
- Communication Inside a Multi Container Pod
- How To Deploy A Multi Container Pod? 
https://k21academy.com/docker-kubernetes/multi-container-pods/