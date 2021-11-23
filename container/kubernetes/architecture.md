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