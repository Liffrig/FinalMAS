namespace mas_wms.Model.Meta;

public interface ILinkCreator<S,T>  
    where T : ILinkCreator<T, S> 
    where S : ILinkCreator<S, T> {
    
    void Link(T withWhat);
    void Unlink(T fromWhat);
}
