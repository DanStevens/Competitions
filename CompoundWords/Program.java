import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class Main {
  public static void main(String[] args) throws IOException {
    BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
    List<String> lines = new ArrayList<String>();
    String line;
    while ((line = reader.readLine()) != null) {
      lines.add(line);
    }
    CompoundWordsSolver solver = new CompoundWordsSolver(lines);
    for (String word : solver.solve()) {
      System.out.println(word);
    }
  }

  public static class CompoundWordsSolver {
    private final Set<String> wordsSet;
    private final List<String> wordsSorted;

    public CompoundWordsSolver(List<String> words) {
      wordsSorted = words;
      wordsSet = new HashSet<String>(wordsSorted);
    }

    public List<String> solve() {
      List<String> result = new ArrayList<String>();
      for (String word : wordsSorted) {
        for (Tuple<String, String> tuple : generateDecompositions(word)) {
          if (wordsSet.contains(tuple.getItem1()) && word.contains(tuple.getItem2())) {
            result.add(word);
            break;
          }
        }
      }
      return result;
    }

    public static List<Tuple<String, String>> generateDecompositions(String word) {
      List<Tuple<String, String>> components = new ArrayList<Tuple<String, String>>();
      for (int splitAt = 1; splitAt < word.length(); splitAt++) {
        components.add(new Tuple<String, String>(word.substring(0, splitAt), word.substring(splitAt)));
      }
      return components;
    }
  }

  public static class Tuple<T1, T2> {
    private final T1 item1;
    private final T2 item2;

    public Tuple(T1 item1, T2 item2) {
      this.item1 = item1;
      this.item2 = item2;
    }

    public T1 getItem1() {
      return item1;
    }

    public T2 getItem2() {
      return item2;
    }
  }
}
