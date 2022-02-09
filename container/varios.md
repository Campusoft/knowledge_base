# Varios

# linkerd

Linkerd is a service mesh for Kubernetes. It makes running services easier and safer by giving you runtime debugging, observability, reliability, and security—all without requiring any changes to your code.


Linkerd uses a Rust “micro-proxy” simply called Linkerd-proxy that we built specifically for the service mesh. Other meshes use different proxies; Envoy is a common choice. But the choice of proxy is an implementation detail. (Edit January 2020: see Why Linkerd Doesn’t Use Envoy for more on why Linkerd uses Linkerd2-proxy rather than Envoy.)

Architecture

- At a high level, Linkerd consists of a control plane and a data plane.
https://linkerd.io/2.11/reference/architecture/


Why Linkerd doesn't use Envoy
- Linkerd doesn’t use Envoy because using Envoy wouldn’t allow us to build the lightest, simplest, and most secure Kubernetes service mesh in the world.
https://linkerd.io/2020/12/03/why-linkerd-doesnt-use-envoy/index.html


# kuma

The universal Envoy service mesh  for distributed service connectivity The open-source control plane for service mesh, delivering security, observability, routing and more
